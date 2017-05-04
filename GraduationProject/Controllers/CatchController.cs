using DHD.ENTITY;
using GraduationProject.Common;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using GraduationProject.Filters;
namespace GraduationProject.Controllers
{
    [CheckLogin]
    public class CatchController : Controller
    {
        DBContext db = new DBContext();
        // GET: Catch
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult CatchData(int SchoolId,int NewTypeId)
        {
            var ips = Grab_ProxyIp();
            //var ips = new List<IpNode>();
            Configure configure = db.Configures.Where(d => d.SchoolId == SchoolId).FirstOrDefault();
            School school = db.Schools.Find(SchoolId);
            NewTypeUrl url = db.NewTypeUrls.Where(d => d.NewTypeId == NewTypeId & d.SchoolId == SchoolId).FirstOrDefault();
            getNews1(configure, url, school.HomePage, ips);
            //Configure school = new Configure();
            //school.ListPath = "//*[@id=\"list\"]/div[1]/ul/li";
            //school.HrefPath = "/li[1]/span[1]/a";
            //school.TitlePath = "//*[@id=\"detail_title\"]/div[2]/span";
            //school.NextPageModule = "http://www.hznu.edu.cn/zhxw/index_2.shtml";


            //Configure school1 = new Configure();
            //school1.ListPath = "//*[@id=\"HDNEWS_MainArea\"]/div[2]/div[2]";
            //school1.HrefPath = "//div[1]/a";
            //school1.TitlePath = "//*[@id=\"HDNEWS_MainArea\"]/div[2]/div[2]/div/div[1]";
            //school1.NextPageModule = "http://www.hdu.edu.cn/news/general_p2";

            //Thread thread = new Thread(new ThreadStart(delegate()
            //{
            //    getNews1(school, "http://www.hznu.edu.cn/zhxw/", "http://www.hznu.edu.cn", ips);
            //}));
            //Thread thread1 = new Thread(new ThreadStart(delegate()
            //{
            //    getNews1(school1, "http://www.hdu.edu.cn/news/general", "http://www.hdu.edu.cn", ips);
            //}));
            //thread.Start();
            //thread1.Start();
            return Json("");
        }
        public JsonResult CatchImg(int schoolId,int newTypeId)
        {
            var schoolnews = db.SchoolNews.Where(d => d.SchoolId == schoolId && d.NewTypeId == newTypeId && d.HaveImg==1 && d.HasCatched==0);
            foreach (SchoolNew schoolNew in schoolnews)
            {
                if (schoolNew.Content == null || schoolNew.Content == "")
                    continue;
                getImage(schoolNew.Id);
                schoolNew.HasCatched = 1;
               
            }
            db.SaveChanges();
           
            return Json("");
        }
        public void getImage(int schoolNewId)
        {
            SchoolNew schoolNew = db.SchoolNews.Find(schoolNewId);
             School school = db.Schools.Find(schoolNew.SchoolId);
            Configure news = db.Configures.Where(d => d.SchoolId == schoolNew.SchoolId).FirstOrDefault(); ;
            NewTypeUrl news1 = db.NewTypeUrls.Where(d => d.NewTypeId == schoolNew.NewTypeId && d.SchoolId == news.SchoolId).FirstOrDefault();


            HtmlDocument doc1 = new HtmlDocument();
            doc1.LoadHtml(schoolNew.Content);
            HtmlNodeCollection imgs = doc1.DocumentNode.SelectNodes("//img");
            int i = 0;
            if (imgs != null)
            {
                foreach (HtmlNode img in imgs)
                {
                    string src = UrlGetter.getImageUrl(img, school.HomePage, news1.Url);
                    string current = DateTime.Now.ToLongDateString();

                    string path = "C:/Users/MikeDai/Documents/Visual Studio 2013/Projects/GraduationProject/GraduationProject/Images/" + schoolNew.Id;
                    if (Directory.Exists(path) == false)//如果不存在就创建file文件夹
                    {
                        Directory.CreateDirectory(path);
                    }
                    string imgurl = path + "/" + schoolNew.Id + i + ".bmp";
                    ImageLoader.DownloadPicture(src, imgurl, -1);
                    Image image = new Image();
                    image.Src = "../../Images/" + schoolNew.Id + "/" + schoolNew.Id + i + ".bmp";
                    image.CreateTime = DateTime.Now;
                    image.SchoolNewId = schoolNewId;
                    db.Images.Add(image);
                    img.Attributes["src"].Value = image.Src;
                    i++;
                }
                schoolNew.Content = doc1.DocumentNode.InnerHtml;
            }
        }
        public  void getNews(Configure news, string Url, string HomePage, List<IpNode> ips)
        {
            //IP被封了？
            HtmlWeb web = new HtmlWeb();
            WebProxy proxy = new WebProxy();
            HtmlDocument doc = web.Load(Url);
            //int Page = 1;
            #region 多线程分页抓取
            int ThreadNumber = 1;
            /*
            if (news.PagePath != null && news.PagePath != "")
            {
                pageText = doc.DocumentNode.SelectSingleNode(news.PagePath).InnerText;
                if (int.TryParse(pageText, out Page))
                {
                    string s = pageText.Split('/')[1].Split('页')[0].Substring(0, 3).Replace("&nbsp;", "");
                    Page = int.Parse(s);
                }
            }
            else
            {
                ThreadNumber = 1;
            }*/

            //string nextPageUrl = news1.NextPagePath;
            ///回车也算text()，难怪获取不到。
            //var page = doc.DocumentNode.SelectNodes(news.PagePath);
            #endregion


            string NewsPage;
            for (int i = 1; i < 100; i++)
            {
                try
                {
                    if (i == 1)
                    {
                        NewsPage = Url;
                    }
                    else
                    {
                        NewsPage = news.NextPageModule.Replace("2", i + "");
                    }
                    doc = web.Load(NewsPage);
                    //HtmlNode next = doc.DocumentNode.SelectNodes("//*[@id=\"fanye\"]/table/tbody/tr/td/a[12]/span").FirstOrDefault();
                    HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(news.ListPath);
                    if (nodes.Count == 0)
                        continue;
                    foreach (HtmlNode child in nodes)
                    {
                        HtmlNode hn = HtmlNode.CreateNode(child.OuterHtml);
                        HtmlNode urlNode = hn.SelectSingleNode(news.HrefPath);
                        //HtmlNode urlNode = doc.DocumentNode.SelectSingleNode(news.HrefPath);
                        if (urlNode != null)
                        {
                            string url = UrlGetter.getDetailUrl(urlNode, HomePage, Url);
                            HtmlWeb web1 = new HtmlWeb();
                            HtmlDocument doc1 = web1.Load(url);
                            // HtmlNode content = doc1.DocumentNode.SelectSingleNode(news.ContentPath);
                            HtmlNode title = doc1.DocumentNode.SelectSingleNode(news.TitlePath);

                            HtmlNode content = doc1.DocumentNode.SelectSingleNode(news.ContentPath);

                            HtmlNode time = doc1.DocumentNode.SelectSingleNode(news.TimePath);

                            HtmlNode author = doc1.DocumentNode.SelectSingleNode(news.AuthorPath);
                            SchoolNew schoolnew = new SchoolNew();
                            if (title != null)
                            {
                                schoolnew.Title = title.InnerText.Trim();
                            }
                            if (content != null)
                            {
                                schoolnew.Content = content.InnerText.Trim();
                            }
                            Regex reg = new Regex("\\d{4}-\\d{2}-\\d{2}");
                            if (time != null)
                            {
                                schoolnew.PublishTime = Convert.ToDateTime(reg.Match(time.InnerText.Trim()).ToString());
                            }
                            Regex reg1 = new Regex("作者.\\w+|来源.\\w+");
                            if (author != null)
                            {
                                schoolnew.Author = reg1.Match(author.InnerText.Trim()).ToString().Split(new char[] { ':', '：' })[1];
                            }
                        }
                    }
                    //Thread.Sleep(3000);
                }
                catch(Exception ex)
                {

                }
            }
        }
        public void getNews1(Configure news, NewTypeUrl NewTypeUrl, string HomePage, List<IpNode> ips)
        {
            //向指定地址发送请求
            string Url = NewTypeUrl.Url;
            ///这里是为了确认抓取的地址是否存在
            //try
            //{
                HttpWebRequest HttpWReq = (HttpWebRequest)WebRequest.Create(Url);
                HttpWebResponse HttpWResp = (HttpWebResponse)HttpWReq.GetResponse();
            StreamReader sr = new StreamReader(HttpWResp.GetResponseStream(), System.Text.Encoding.GetEncoding("UTF-8"));
            HtmlDocument doc = new HtmlDocument();
            string NewsPage;
            int index = -1;
            int count = ips.Count;
            for (int i = 1; i < 10; i++)
            {
                if (i == 1)
                {
                    NewsPage = Url;
                }
                else
                {
                    NewsPage = news.NextPageModule.Replace("2", i + "");
                }
                HttpWReq = (HttpWebRequest)WebRequest.Create(NewsPage);
                index++;
                IpNode ip = ips[(index) % count];
                WebProxy proxyObject = new WebProxy(ip.ip, ip.port);
                HttpWReq.Proxy = proxyObject;
                try
                {
                    HttpWResp = (HttpWebResponse)HttpWReq.GetResponse();
                    sr = new StreamReader(HttpWResp.GetResponseStream(), System.Text.Encoding.GetEncoding("UTF-8"));
                    doc.Load(sr);
                }
                catch(Exception ex)
                {
                    i--;
                    continue;
                }
                //HtmlNode next = doc.DocumentNode.SelectNodes("//*[@id=\"fanye\"]/table/tbody/tr/td/a[12]/span").FirstOrDefault();
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(news.ListPath);
                if (nodes.Count == 0)
                    continue;

                foreach (HtmlNode child in nodes)
                {
                    HtmlNode hn = HtmlNode.CreateNode(child.OuterHtml);
                    HtmlNode urlNode = hn.SelectSingleNode(news.HrefPath);
                    //HtmlNode urlNode = doc.DocumentNode.SelectSingleNode(news.HrefPath);
                    try
                    {
                        if (urlNode != null && urlNode.InnerHtml != null)
                        {
                            string url = UrlGetter.getDetailUrl(urlNode, HomePage, Url);
                            SchoolNew schoolnew = new SchoolNew();

                            HtmlWeb web1 = new HtmlWeb();
                            HtmlDocument doc1 = web1.Load(url);
                            // HtmlNode content = doc1.DocumentNode.SelectSingleNode(news.ContentPath);
                            HtmlNode title = doc1.DocumentNode.SelectSingleNode(news.TitlePath);

                            HtmlNode content = doc1.DocumentNode.SelectSingleNode(news.ContentPath);
                            HtmlNode test = HtmlNode.CreateNode(content.OuterHtml);

                            ///为了界面好看，先只抓有图片的
                            if (test.SelectNodes("//img") != null)
                                schoolnew.HaveImg = 1;
                            else
                            {
                                schoolnew.HaveImg = 0;
                            }

                            HtmlNode time = doc1.DocumentNode.SelectSingleNode(news.TimePath);

                            HtmlNode author = doc1.DocumentNode.SelectSingleNode(news.AuthorPath);
                           
                            if (title != null)
                            {
                                schoolnew.Title = title.InnerText;
                            }
                            if (content != null)
                            {
                                schoolnew.Content = content.InnerHtml;
                             
                            }
                            Regex reg = new Regex("\\d{4}-\\d{2}-\\d{2}");
                            if (time != null)
                            {
                                schoolnew.PublishTime = Convert.ToDateTime(reg.Match(time.InnerText.Trim()).ToString());
                            }
                            Regex reg1 = new Regex("作者.\\w+|来源.\\w+");
                            if (author != null)
                            {
                                schoolnew.Author = reg1.Match(author.InnerText.Trim()).ToString().Split(new char[] { ':', '：' })[1];
                            }
                            
                           
                            schoolnew.SchoolId = news.SchoolId;
                            schoolnew.NewTypeId = NewTypeUrl.NewTypeId;
                           
                            db.SchoolNews.Add(schoolnew);
                        }
                    }
                    catch(Exception ex)
                    {
                        continue;
                    }
                }
            }
            db.SaveChanges();
            //}
            //catch (Exception ex)
            //{
            //    return;
            //}
        }
        public  bool validate(string str, int port)
        {
            try
            {
                //设置代理IP
                WebProxy proxyObject = new WebProxy(str, port);
                //向指定地址发送请求
                HttpWebRequest HttpWReq = (HttpWebRequest)WebRequest.Create("http://www.baidu.com");
                HttpWReq.Proxy = proxyObject;
                HttpWebResponse HttpWResp = (HttpWebResponse)HttpWReq.GetResponse();
                HttpWReq.Timeout = 2000;
                //HttpWReq.ReadWriteTimeout = 10000;
                StreamReader sr = new StreamReader(HttpWResp.GetResponseStream(), System.Text.Encoding.GetEncoding("UTF-8"));
                string xmlContent = sr.ReadToEnd().Trim();
                sr.Close();
                HttpWResp.Close();
                HttpWReq.Abort();
                Console.WriteLine("ip:" + str + " 端口号:" + port + " 有效的代理ip");
            }
            catch (Exception)
            {
                Console.WriteLine("ip:" + str + " 端口号:" + port + " 无效的代理ip");
                return false;
            }
            return true;
        }
        #region 生产IP 代理 对象
        private  List<IpNode> Grab_ProxyIp()
        {

            string Url = "http://www.xicidaili.com/nn/1";
            string listPath = "//*[@id=\"ip_list\"]/tr";
            string ipPath = "//tr/td[2]";
            string portPath = "//tr/td[3]";
            //List<IpNode> ips = printIp(Url, listPath, ipPath, portPath);

            Url = "http://www.iphai.com/free/ng";
            listPath = "/html/body/div[2]/div[2]/table/tr";
            ipPath = "//tr/td[1]";
            portPath = "//tr/td[2]";
            //这个验证太耗时间了，得想个办法搞定
            //List<IpNode> ips = printIp(Url, listPath, ipPath, portPath);
            List<IpNode> ips = new List<IpNode>();
            ips.Add(new IpNode("58.49.109.166", 80));
            ips.Add(new IpNode("121.193.143.249", 80));
            
            //ips.AddRange(printIp(Url, listPath, ipPath, portPath));

            Url = "http://www.ip3366.net/free/?stype=1";
            listPath = "//*[@id=\"list\"]/table/tbody/tr";
            ipPath = "//tr/td[1]";
            portPath = "//tr/td[2]";
            //ips.AddRange(printIp(Url, listPath, ipPath, portPath));

            Url = "http://www.66ip.cn/index.html";
            listPath = "//*[@id=\"main\"]/div/div[1]/table/tr";
            ipPath = "//tr/td[1]";
            portPath = "//tr/td[2]";
            //ips.AddRange(printIp(Url, listPath, ipPath, portPath));

            return ips;
        }
        public  List<IpNode> printIp(string Url, string listPath, string ipPath, string portPath)
        {
            HtmlWeb web = new HtmlWeb();

            HtmlDocument doc = web.Load(Url);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(listPath);
            List<IpNode> ips = new List<IpNode>();
            foreach (HtmlNode child in nodes)
            {
                HtmlNode hn = HtmlNode.CreateNode(child.OuterHtml);
                HtmlNode ipNode = hn.SelectSingleNode(ipPath);
                HtmlNode portNode = hn.SelectSingleNode(portPath);
                Regex reg = new Regex("^(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|[1-9])\\."
+ "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\."
+ "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\."
+ "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)$");
                if (ipNode != null && portNode != null)
                {
                    string trueIp = reg.Match(ipNode.InnerText.Trim()).ToString();

                    if (validate(trueIp, int.Parse(portNode.InnerText)))
                    {
                        ips.Add(new IpNode(trueIp, int.Parse(portNode.InnerText)));
                    }
                }
                if (ips.Count > 2)
                {
                    break;
                }
            }
            //eventX.WaitOne();
            return ips;
        }
        #endregion
        public class IpNode
        {
            public string ip { get; set; }
            public int port { get; set; }
            public IpNode(string ip, int port)
            {
                this.ip = ip;
                this.port = port;
            }
        }
    }
}
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;

namespace GraduationProject.Common
{
    public class ForeachClass
    {
        /// <summary>
        /// C#反射遍历对象属性
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="model">对象</param>
        public static void ForeachClassProperties<T>(T model)
        {
            Type t = model.GetType();
            PropertyInfo[] PropertyList = t.GetProperties();
            foreach (PropertyInfo item in PropertyList)
            {
                string name = item.Name;
                var displayName = item.GetCustomAttribute<DisplayNameAttribute>();
                object value = item.GetValue(model, null);
            }
        }
        public static string[] GetTableHeaders<T>()
        {
            Type t = typeof(T);
            PropertyInfo[] PropertyList = t.GetProperties();
            
            List<String> headers = new List<string>();
            int i = 0;
            foreach (PropertyInfo item in PropertyList)
            {
                string name = item.Name;
                var displayName = item.GetCustomAttribute<DisplayNameAttribute>();
                if (displayName != null)
                    headers.Add(displayName.DisplayName);
              
            }
            string[] tableHeaders = new string[headers.Count()];
            foreach (var header in headers)
            {
                tableHeaders[i] = header;
                i++;
            }  
            return tableHeaders;
        }
        public static List<AttributeObject> GetTableAttribute<T>()
        {
            Type t = typeof(T);
            PropertyInfo[] PropertyList = t.GetProperties();

            List<AttributeObject> list = new List<AttributeObject>();
            int i = 0;
            foreach (PropertyInfo item in PropertyList)
            {
                string name = item.Name;
                var displayName = item.GetCustomAttribute<DisplayNameAttribute>();
                if (displayName != null)
                {
                    AttributeObject attribute = new AttributeObject();
                    attribute.DisplayName = displayName.DisplayName;
                    attribute.Name = name;
                    list.Add(attribute);
                }

            }

            return list;
        }
        public class AttributeObject
        {
            public string Name { get; set; }
            public string DisplayName { get; set; }
            public string Value { get; set; }
        }
    }
    public class ImageLoader
    {
        public static bool DownloadPicture(string picUrl, string savePath, int timeOut)
        {
            bool value = false;
            WebResponse response = null;
            Stream stream = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(picUrl);
                if (timeOut != -1) request.Timeout = timeOut;
                response = request.GetResponse();
                stream = response.GetResponseStream();
                if (!response.ContentType.ToLower().StartsWith("text/"))
                    value = SaveBinaryFile(response, savePath);
            }
            finally
            {
                if (stream != null) stream.Close();
                if (response != null) response.Close();
            }
            return value;
        }
        private static bool SaveBinaryFile(WebResponse response, string savePath)
        {
            bool value = false;
            byte[] buffer = new byte[1024];
            Stream outStream = null;
            Stream inStream = null;
            try
            {
                if (File.Exists(savePath))
                    File.Delete(savePath);
                outStream = File.Create(savePath);
                inStream = response.GetResponseStream();
                int l;
                do
                {
                    l = inStream.Read(buffer, 0, buffer.Length);
                    if (l > 0) outStream.Write(buffer, 0, l);
                } while (l > 0);
                value = true;
            }
            finally
            {
                if (outStream != null) outStream.Close();
                if (inStream != null) inStream.Close();
            }
            return value;
        }
    }
    public class UrlGetter
    {
        public static string getDetailUrl(HtmlNode urlNode, String HomePage, String Url)
        {
            string href = urlNode.Attributes["href"].Value;
            if (urlNode.Attributes["onclick"] == null)
            {
                if (href.StartsWith("http"))
                {
                    return href;
                }
                //从根目录开始
                else if (href.StartsWith("/"))
                {
                    return HomePage + href;
                }
                else if (href.StartsWith("../"))
                {
                    string dir = Url.Substring(0, Url.LastIndexOf("/"));

                    string s = href.Substring(2, href.Length - 2);
                    return dir + s;
                }
                else
                {
                    string dir = Url.Substring(0, Url.LastIndexOf("/"));
                    return dir + "/" + href;
                }
            }
            else
            {
                string onclick = urlNode.Attributes["onclick"].Value;
                int start = onclick.IndexOf("(");
                int end = onclick.IndexOf(")");
                int length = end - start - 1;
                href = onclick.Substring(start + 2, length - 2);
                if (href.StartsWith("http"))
                {
                    return href;
                }
                //从根目录开始
                else if (href.StartsWith("/"))
                {
                    return HomePage + href;
                }
                else if (href.StartsWith("../"))
                {
                    string dir = Url.Substring(0, Url.LastIndexOf("/"));
                    string s = href.Substring(2, href.Length - 2);
                    return dir + s;
                }
                else
                {
                    string dir = Url.Substring(0, Url.LastIndexOf("/"));

                    return dir + "/" + href;
                }
            }
        }
         public static string getImageUrl(HtmlNode urlNode, String HomePage, String Url)
        {
            string src = urlNode.Attributes["src"].Value;
            if (urlNode.Attributes["onclick"] == null)
            {
                if (src.StartsWith("http"))
                {
                    return src;
                }
                //从根目录开始
                else if (src.StartsWith("/"))
                {
                    return HomePage + src;
                }
                else if (src.StartsWith("../"))
                {
                    string dir = Url.Substring(0, Url.LastIndexOf("/"));

                    string s = src.Substring(2, src.Length - 2);
                    return dir + s;
                }
                else
                {
                    string dir = Url.Substring(0, Url.LastIndexOf("/"));
                    return dir + "/" + src;
                }
            }
            else
            {
                string onclick = urlNode.Attributes["onclick"].Value;
                int start = onclick.IndexOf("(");
                int end = onclick.IndexOf(")");
                int length = end - start - 1;
                src = onclick.Substring(start + 2, length - 2);
                if (src.StartsWith("http"))
                {
                    return src;
                }
                //从根目录开始
                else if (src.StartsWith("/"))
                {
                    return HomePage + src;
                }
                else if (src.StartsWith("../"))
                {
                    string dir = Url.Substring(0, Url.LastIndexOf("/"));
                    string s = src.Substring(2, src.Length - 2);
                    return dir + s;
                }
                else
                {
                    string dir = Url.Substring(0, Url.LastIndexOf("/"));

                    return dir + "/" + src;
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class JsonModel
    {
        public Object data { get; set; }
        public string msg { get; set; }
        public string code { get; set; }
        public string[] tableHeaders { get; set; }
        public int totalCount { get; set; }
        public JsonModel(string msg, Object data, string[] tableHeaders,int totalCount)
        {
            this.data = data;
            this.msg = msg;
            this.code = "10001";
            this.tableHeaders = tableHeaders;
            this.totalCount = totalCount;
        }
        public JsonModel(string msg, Object data, string[] tableHeaders)
        {
            this.data = data;
            this.msg = msg;
            this.code = "10001";
            this.tableHeaders = tableHeaders;
        }
        public JsonModel(string msg, Object data)
        {
            this.data = data;
            this.msg = msg;
            this.code = "10001";
        }
        public JsonModel(string msg)
        {
            this.msg = msg;
            this.code = "10002";
        }
    }
}
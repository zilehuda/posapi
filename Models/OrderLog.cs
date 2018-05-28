using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PosApi.Models
{
    public class OrderLog
    {
        public string orderid { get; set; }
        public string productid { get; set; }
        public string qty { get; set; }
        public string total { get; set; }
        public string date { get; set; }
    }
}
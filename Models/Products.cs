using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PosApi.Models
{
    public class Products
    {

        public string id { get; set; }
        public string name { get; set; }
        public string qty { get; set; }
        public string price { get; set; }
        public string vendid { get; set; }
    }
}
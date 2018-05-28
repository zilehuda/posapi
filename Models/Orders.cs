using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PosApi.Models
{
    public class Orders
    {
        public string orderId { get; set; }
        public string productId { get; set; }
        public string qty { get; set; }
        public string total { get; set; }
    }
}
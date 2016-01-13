using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InClass3_REST.Models
{
    public class ProductModel
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Mfg { get; set; }
        public string Vendor { get; set; }
        public decimal Price { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InClass3_REST.Models
{
    public class ProductModelBuilder
    {
        public static ProductModel create(Product prod)
        {
            ProductModel prodModel = new ProductModel();
            prodModel.ProductID = prod.productID;
            prodModel.Name = prod.name;
            prodModel.Mfg = prod.mfg;
            prodModel.Vendor = prod.vendor;
            prodModel.Price = (decimal)prod.price;
            return prodModel;
        }
    }
}
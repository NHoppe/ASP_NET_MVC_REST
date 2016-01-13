using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Script.Serialization;
using InClass3_REST;
using InClass3_REST.Models;
using System.Web.Http.Cors;

namespace InClass3_REST.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProductsController : ApiController
    {
        private A00964856_FoodStoreEntities db = new A00964856_FoodStoreEntities();

        // GET: api/Products
        public IHttpActionResult GetProducts()
        {
            List<ProductModel> listOfProducts = new List<ProductModel>();

            foreach (var prod in db.Products)
            {
                ProductModel prodModel = ProductModelBuilder.create(prod);
                listOfProducts.Add(prodModel);
            }

            var jsonObj = new JavaScriptSerializer().Serialize(listOfProducts);

            if (jsonObj != null)
            {
                return Ok(jsonObj);
            }
            return NotFound();
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(ProductModelBuilder.create(product));
        }

        // PUT: api/Products/
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, ProductModel prodModel)
        {
            Product product = db.Products.Find(prodModel.ProductID);
            if (product == null)
            {
                return NotFound();
            }

            product.price = prodModel.Price;

            db.SaveChanges();

            return Ok(new JavaScriptSerializer().Serialize(prodModel));
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            var invoices = from i in db.Invoices
                           where i.Products.Any(p => p.productID == product.productID)
                           select i;

            var purchaseOrders = from po in db.PurchaseOrders
                                 where po.Products.Any(p => p.productID == product.productID)
                                 select po;

            foreach (var purchaseOrder in purchaseOrders)
            {
                // Remove product from PurchaseOrder
                purchaseOrder.Products.Remove(product);
            }

            foreach (var invoice in invoices)
            {
                var invoicesWithQty = from iwq in db.ProductInvoiceWithQuantities
                                      where iwq.productID == product.productID
                                                && iwq.invoiceNum == invoice.invoiceNum
                                      select iwq;

                // Remove row from ProductInvoiceWithQuantities table
                db.ProductInvoiceWithQuantities.RemoveRange(invoicesWithQty);

                // Remove row from ProductInvoice table
                invoice.Products.Remove(product);
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.productID == id) > 0;
        }
    }
}
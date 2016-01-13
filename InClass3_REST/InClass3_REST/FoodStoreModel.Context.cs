﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InClass3_REST
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class A00964856_FoodStoreEntities : DbContext
    {
        public A00964856_FoodStoreEntities()
            : base("name=A00964856_FoodStoreEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductInvoiceWithQuantity> ProductInvoiceWithQuantities { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
    
        public virtual ObjectResult<spFindEmployee_Result> spFindEmployee(string lname)
        {
            var lnameParameter = lname != null ?
                new ObjectParameter("lname", lname) :
                new ObjectParameter("lname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spFindEmployee_Result>("spFindEmployee", lnameParameter);
        }
    
        public virtual ObjectResult<spFindProduct_Result> spFindProduct(Nullable<int> productID)
        {
            var productIDParameter = productID.HasValue ?
                new ObjectParameter("productID", productID) :
                new ObjectParameter("productID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spFindProduct_Result>("spFindProduct", productIDParameter);
        }
    
        public virtual ObjectResult<spGetAllProducts_Result> spGetAllProducts()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetAllProducts_Result>("spGetAllProducts");
        }
    
        public virtual ObjectResult<spProductDetail_Result> spProductDetail(string name, string vendor)
        {
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            var vendorParameter = vendor != null ?
                new ObjectParameter("vendor", vendor) :
                new ObjectParameter("vendor", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spProductDetail_Result>("spProductDetail", nameParameter, vendorParameter);
        }
    
        public virtual ObjectResult<spStoresByRegion_Result> spStoresByRegion(string region)
        {
            var regionParameter = region != null ?
                new ObjectParameter("region", region) :
                new ObjectParameter("region", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spStoresByRegion_Result>("spStoresByRegion", regionParameter);
        }
    }
}

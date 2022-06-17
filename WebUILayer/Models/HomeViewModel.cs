using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityLayer.Entity;

namespace WebUILayer.Models
{
    public class HomeViewModel
    {
        public List<ProductList> LatestPRoduct { get; set; } = new List<ProductList>();
        public List<SaleList> LatestSales { get; set; } = new List<SaleList>();

        public Sale Sale { get; set; }
       
    }

    public class ProductList
    {
        public Product Product { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }

    public class SaleList
    {
        public Sale Sale { get; set; }
        public List<EndUser> EndUsers { get; set; }
        public List<Product> Products { get; set; }
    }

    
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityLayer.Entity;

namespace WebUILayer.Models
{
    public class SalesViewModel
    {
        public List<SalesItem> SalesItems { get; set; } = new List<SalesItem>();

    }

    public class SalesItem
    {
        public Sale Sale { get; set; }
        public Product Product { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
}
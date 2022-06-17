using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityLayer.Entity;

namespace WebUILayer.Models
{
    public class OrderHistoryViewModel
    {
        public List<OrderHistoryItem> OrderHistoryItems { get; set; } = new List<OrderHistoryItem>();
    }


    public class OrderHistoryItem
    {
        public Product Product { get; set; }
        public Sale Sale { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<Cart> Carts { get; set; }

    }
}
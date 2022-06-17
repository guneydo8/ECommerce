using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityLayer.Entity;

namespace WebUILayer.Models
{
    public class ChartViewModel
    {
        public List<Cart> Carts { get; set; }

        public List<CartListItemModel> CartListItems { get; set; } = new List<CartListItemModel>(); 
       
    }

    public class CartListItemModel
    {
        public Cart Cart { get; set; }
        public Product Product { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
}
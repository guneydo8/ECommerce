using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityLayer.Entity;

namespace WebUILayer.Models
{
    public class CheckViewModel
    {
        public List<EndUser> EndUsers { get; set; }
        public List<CheckListItem> Products { get; set; } = new List<CheckListItem>();
   

    }

    public class CheckListItem
    {
        public Cart Cart { get; set; }
        public Product Product { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
}
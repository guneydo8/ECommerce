using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUILayer.Models
{
    public class WhistlistViewModel
    {
        public List<WhistlistItemModel> WhistlistItems { get; set; } = new List<WhistlistItemModel>();
    }

    public class WhistlistItemModel
    {
        public Product Product { get; set; }
        public Whistlist Whistlist { get; set; }
        public List<ProductImage> ProductImages { get; set; }

    }
}
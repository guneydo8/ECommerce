using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityLayer.Entity;

namespace WebUILayer.Models
{
    public class CompareViewModel
    {
        public List<CompareListItemModel> CompareListItemModels { get; set; } = new List<CompareListItemModel>();
       

    }


    public class CompareListItemModel
    {
        public Product Product { get; set; }
        public Compare Compare { get; set; }
        public List<ProductImage> ProductImages { get; set; }

    }
}
using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUILayer.Models
{
    public class ProductListModel
    {
        public List<ProductListItemModel> Products { get; set; } = new List<ProductListItemModel>();
        public List<ProductBrand> Brands { get; set; }
        public List<ProductListItemModel> LatestProduct { get; set; } = new List<ProductListItemModel>();
        public List<ProductCategory> ProductCategories { get; set; }
        public List<Product> SinleProduct { get; set; }
        public List<CommentListItemModel> ProductComments { get; set; }
        
      



     
    }
    public class ProductListItemModel
    {
        public Product Product { get; set; }
        public List<ProductImage> Images { get; set; }

  
    }

    public class CommentListItemModel
    {
        public Product Product { get; set; }

        public List<ProductComment> ProductComments { get; set; }
    }
}
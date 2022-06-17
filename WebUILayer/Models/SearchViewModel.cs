using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityLayer.Entity;

namespace WebUILayer.Models
{
    public class SearchViewModel
    {
        //public List<Product> Products { get; set; }
        public List<ProductItemModel> ProductItemModels { get; set; } = new List<ProductItemModel>();
        public List<ProductBrand> ProductBrands { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }

    }



    public class ProductItemModel
    {
        public Product Product { get; set; }
        public List<ProductImage> Images { get; set; }


    }


}
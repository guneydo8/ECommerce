using EntityLayer.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entity
{
    public class Product:LowerBase
    {
        public string ProductCode { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public int Stock { get; set; }

        public int ProductBrandId { get; set; }
        public virtual ProductBrand ProductBrand { get; set; }

        public int ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }


        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<ProductColor> ProductColors { get; set; }
        public ICollection<ProductComment> ProductComments { get; set; }
        public ICollection<Whistlist> Whistlists { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<Sale> Sales { get; set; }
        public ICollection<Compare> Compares { get; set; }






    }
}

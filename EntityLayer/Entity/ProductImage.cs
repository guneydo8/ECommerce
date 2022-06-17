using EntityLayer.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entity
{
     public class ProductImage:LowerBase
    {
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }
    }
}

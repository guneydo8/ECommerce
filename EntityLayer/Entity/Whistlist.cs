using EntityLayer.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entity
{
    public class Whistlist:LowerBase
    {
        public decimal Price { get; set; }
      
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int EndUserId { get; set; }
        public virtual EndUser EndUser { get; set; }
    }
}

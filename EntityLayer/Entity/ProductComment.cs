using EntityLayer.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entity
{
    public class ProductComment:LowerBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }

        public int EndUserId { get; set; }
        public virtual EndUser EndUser { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        
    }
}

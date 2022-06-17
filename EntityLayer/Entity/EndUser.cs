using EntityLayer.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entity
{
   public class EndUser:LowerBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }
        public DateTime BornDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }


        public ICollection<Whistlist> Whistlists { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<Sale> Sales { get; set; }
        public ICollection<ProductComment> ProductComments { get; set; }
        public ICollection<Compare> Compares { get; set; }

    }
}

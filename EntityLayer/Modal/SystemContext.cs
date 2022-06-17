using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Modal
{
    public class SystemContext:DbContext
    {
        public SystemContext() : base("name=system")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }



        public DbSet<Faq> Faqs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<EndUser> EndUsers { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Whistlist> Whistlists { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<ContactInformation> ContactInformations { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Compare> Compares { get; set; }
        public DbSet<Sale> Sales { get; set; }
      
    }
}

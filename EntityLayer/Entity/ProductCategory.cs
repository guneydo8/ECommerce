﻿using EntityLayer.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entity
{
    public class ProductCategory:LowerBase
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyInventory.Models
{
    public class StoreViewModel
    {
        public List<Product> ProductList { get; set; }

        public List<Category> CategoryList { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual Student Student { get; set; }
    }
}

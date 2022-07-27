using System;
using System.Collections.Generic;

namespace Aciktim.Models
{
    public partial class Category
    {
        public Category()
        {
            ProductCategories = new HashSet<ProductCategory>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;

        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}

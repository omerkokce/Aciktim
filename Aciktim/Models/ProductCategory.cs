using System;
using System.Collections.Generic;

namespace Aciktim.Models
{
    public partial class ProductCategory
    {
        public int Pcid { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}

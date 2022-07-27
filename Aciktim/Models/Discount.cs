using System;
using System.Collections.Generic;

namespace Aciktim.Models
{
    public partial class Discount
    {
        public Discount()
        {
            Products = new HashSet<Product>();
        }

        public int DiscountId { get; set; }
        public int Percentage { get; set; }
        public DateTime Expiration { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}

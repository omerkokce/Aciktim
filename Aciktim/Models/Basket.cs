using System;
using System.Collections.Generic;

namespace Aciktim.Models
{
    public partial class Basket
    {
        public Basket()
        {
            BasketProducts = new HashSet<BasketProduct>();
        }

        public int BasketId { get; set; }
        public int ClientId { get; set; }

        public virtual Client Client { get; set; } = null!;
        public virtual ICollection<BasketProduct> BasketProducts { get; set; }
    }
}

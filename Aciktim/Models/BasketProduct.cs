using System;
using System.Collections.Generic;

namespace Aciktim.Models
{
    public partial class BasketProduct
    {
        public int Bpid { get; set; }
        public int ClientId { get; set; }
        public int ProductId { get; set; }

        public virtual Client Client { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}

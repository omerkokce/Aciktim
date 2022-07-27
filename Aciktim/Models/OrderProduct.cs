using System;
using System.Collections.Generic;

namespace Aciktim.Models
{
    public partial class OrderProduct
    {
        public int Opid { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}

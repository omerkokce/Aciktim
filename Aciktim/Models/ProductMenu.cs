using System;
using System.Collections.Generic;

namespace Aciktim.Models
{
    public partial class ProductMenu
    {
        public int Pmid { get; set; }
        public int ProductId { get; set; }
        public int MenuId { get; set; }

        public virtual Menu Menu { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}

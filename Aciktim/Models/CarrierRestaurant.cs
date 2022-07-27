using System;
using System.Collections.Generic;

namespace Aciktim.Models
{
    public partial class CarrierRestaurant
    {
        public int Crid { get; set; }
        public int CarrierId { get; set; }
        public int RestaurantId { get; set; }

        public virtual Carrier Carrier { get; set; } = null!;
        public virtual Restaurant Restaurant { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;

namespace Aciktim.Models
{
    public partial class Carrier
    {
        public Carrier()
        {
            CarrierRestaurants = new HashSet<CarrierRestaurant>();
        }

        public int CarrierId { get; set; }
        public string CarrierName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public int RoleId { get; set; }
        public int AddressId { get; set; }
        public string Password { get; set; } = null!;
        public string EMail { get; set; } = null!;

        public virtual Address Address { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<CarrierRestaurant> CarrierRestaurants { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Aciktim.Models
{
    public partial class ApartmentNumber
    {
        public ApartmentNumber()
        {
            Addresses = new HashSet<Address>();
        }

        public int ApartmentNumberId { get; set; }
        public string Name { get; set; } = null!;
        public int ApartmentId { get; set; }

        public virtual Apartment Apartment { get; set; } = null!;
        public virtual ICollection<Address> Addresses { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Aciktim.Models
{
    public partial class Apartment
    {
        public Apartment()
        {
            Addresses = new HashSet<Address>();
            ApartmentNumbers = new HashSet<ApartmentNumber>();
        }

        public int ApartmentId { get; set; }
        public string Name { get; set; } = null!;
        public int StreetId { get; set; }

        public virtual Street Street { get; set; } = null!;
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<ApartmentNumber> ApartmentNumbers { get; set; }
    }
}

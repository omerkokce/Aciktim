using System;
using System.Collections.Generic;

namespace Aciktim.Models
{
    public partial class Street
    {
        public Street()
        {
            Addresses = new HashSet<Address>();
            Apartments = new HashSet<Apartment>();
        }

        public int StreetId { get; set; }
        public string Name { get; set; } = null!;
        public int NeighbourhoodId { get; set; }

        public virtual Neighbourhood Neighbourhood { get; set; } = null!;
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Apartment> Apartments { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Aciktim.Models
{
    public partial class Address
    {
        public Address()
        {
            Carriers = new HashSet<Carrier>();
            ClientAddresses = new HashSet<ClientAddress>();
            Orders = new HashSet<Order>();
            Restaurants = new HashSet<Restaurant>();
        }

        public int AddressId { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public int NeighbourhoodId { get; set; }
        public int StreetId { get; set; }
        public int ApartmentId { get; set; }
        public int ApartmentNumberId { get; set; }
        public string? AddressName { get; set; }

        public virtual Apartment Apartment { get; set; } = null!;
        public virtual ApartmentNumber ApartmentNumber { get; set; } = null!;
        public virtual City City { get; set; } = null!;
        public virtual Country Country { get; set; } = null!;
        public virtual Neighbourhood Neighbourhood { get; set; } = null!;
        public virtual State State { get; set; } = null!;
        public virtual Street Street { get; set; } = null!;
        public virtual ICollection<Carrier> Carriers { get; set; }
        public virtual ICollection<ClientAddress> ClientAddresses { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Restaurant> Restaurants { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Aciktim.Models
{
    public partial class City
    {
        public City()
        {
            Addresses = new HashSet<Address>();
            States = new HashSet<State>();
        }

        public int CityId { get; set; }
        public string Name { get; set; } = null!;
        public int CountryId { get; set; }

        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<State> States { get; set; }
    }
}

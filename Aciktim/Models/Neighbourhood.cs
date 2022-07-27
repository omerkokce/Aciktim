using System;
using System.Collections.Generic;

namespace Aciktim.Models
{
    public partial class Neighbourhood
    {
        public Neighbourhood()
        {
            Addresses = new HashSet<Address>();
            Streets = new HashSet<Street>();
        }

        public int NeighbourhoodId { get; set; }
        public string Name { get; set; } = null!;
        public int StateId { get; set; }

        public virtual State State { get; set; } = null!;
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Street> Streets { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Aciktim.Models
{
    public partial class State
    {
        public State()
        {
            Neighbourhoods = new HashSet<Neighbourhood>();
        }

        public int StateId { get; set; }
        public string Name { get; set; } = null!;
        public int CityId { get; set; }

        public virtual City City { get; set; } = null!;
        public virtual ICollection<Neighbourhood> Neighbourhoods { get; set; }
    }
}

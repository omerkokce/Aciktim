using System;
using System.Collections.Generic;

namespace Aciktim.Models
{
    public partial class Manager
    {
        public Manager()
        {
            Restaurants = new HashSet<Restaurant>();
        }

        public int ManagerId { get; set; }
        public string ManagerName { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Tcno { get; set; } = null!;

        public virtual ICollection<Restaurant> Restaurants { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Aciktim.Models
{
    public partial class Role
    {
        public Role()
        {
            Carriers = new HashSet<Carrier>();
            Clients = new HashSet<Client>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;

        public virtual ICollection<Carrier> Carriers { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Restaurant> Restaurants { get; set; }
    }
}

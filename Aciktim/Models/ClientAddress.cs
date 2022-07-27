using System;
using System.Collections.Generic;

namespace Aciktim.Models
{
    public partial class ClientAddress
    {
        public int Caid { get; set; }
        public int AddressId { get; set; }
        public int ClientId { get; set; }

        public virtual Address Address { get; set; } = null!;
        public virtual Client Client { get; set; } = null!;
    }
}

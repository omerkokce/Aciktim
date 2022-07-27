﻿using System;
using System.Collections.Generic;

namespace Aciktim.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public int OrderId { get; set; }
        public int ClientId { get; set; }
        public int AddressId { get; set; }
        public DateTime? Date { get; set; }

        public virtual Address Address { get; set; } = null!;
        public virtual Client Client { get; set; } = null!;
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
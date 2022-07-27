using System;
using System.Collections.Generic;

namespace Aciktim.Models
{
    public partial class Card
    {
        public int CardId { get; set; }
        public string CardNumber { get; set; } = null!;
        public string Cvv { get; set; } = null!;
        public DateTime ExprationDate { get; set; }
        public int ClientId { get; set; }

        public virtual Client Client { get; set; } = null!;
    }
}

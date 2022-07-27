using System;
using System.Collections.Generic;

namespace Aciktim.Models
{
    public partial class ClientFavorite
    {
        public int FavoriteId { get; set; }
        public int ClientId { get; set; }
        public int RestaurantId { get; set; }

        public virtual Client Client { get; set; } = null!;
        public virtual Restaurant Restaurant { get; set; } = null!;
    }
}

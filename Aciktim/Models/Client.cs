using System;
using System.Collections.Generic;

namespace Aciktim.Models
{
    public partial class Client
    {
        public Client()
        {
            Baskets = new HashSet<BasketProduct>();
            Cards = new HashSet<Card>();
            ClientAddresses = new HashSet<ClientAddress>();
            ClientFavorites = new HashSet<ClientFavorite>();
            Comments = new HashSet<Comment>();
            Orders = new HashSet<Order>();
        }

        public int ClientId { get; set; }
        public string EMail { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ClientName { get; set; } = null!;
        public int RoleId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<BasketProduct> Baskets { get; set; }
        public virtual ICollection<BasketMenu> CBasketMenus { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<ClientAddress> ClientAddresses { get; set; }
        public virtual ICollection<ClientFavorite> ClientFavorites { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}

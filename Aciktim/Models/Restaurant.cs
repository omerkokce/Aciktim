using System;
using System.Collections.Generic;

namespace Aciktim.Models
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            CarrierRestaurants = new HashSet<CarrierRestaurant>();
            ClientFavorites = new HashSet<ClientFavorite>();
            Comments = new HashSet<Comment>();
            Menus = new HashSet<Menu>();
            Products = new HashSet<Product>();
        }

        public int RestaurantId { get; set; }
        public string EMail { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public int ManagerId { get; set; }
        public int RoleId { get; set; }
        public int AddressId { get; set; }
        public string RestaurantName { get; set; } = null!;
        public int? ImageId { get; set; }

        public virtual Address Address { get; set; } = null!;
        public virtual Image? Image { get; set; }
        public virtual Manager Manager { get; set; } = null!;
        public virtual ICollection<CarrierRestaurant> CarrierRestaurants { get; set; }
        public virtual ICollection<ClientFavorite> ClientFavorites { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}

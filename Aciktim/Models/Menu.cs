using System;
using System.Collections.Generic;

namespace Aciktim.Models
{
    public partial class Menu
    {
        public Menu()
        {
            ProductMenus = new HashSet<ProductMenu>();
        }

        public int MenuId { get; set; }
        public string MenuName { get; set; } = null!;
        public decimal Price { get; set; }
        public int RestaurantId { get; set; }
        public int? ImageId { get; set; }

        public virtual Image? Image { get; set; }
        public virtual Restaurant Restaurant { get; set; } = null!;
        public virtual ICollection<ProductMenu> ProductMenus { get; set; }
        public virtual ICollection<BasketMenu> BasketMenus { get; set; }
        public virtual ICollection<OrderMenu> OrderMenus { get; set; }
    }
}

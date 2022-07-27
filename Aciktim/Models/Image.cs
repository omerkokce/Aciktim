using System;
using System.Collections.Generic;

namespace Aciktim.Models
{
    public partial class Image
    {
        public Image()
        {
            Menus = new HashSet<Menu>();
            Products = new HashSet<Product>();
            Restaurants = new HashSet<Restaurant>();
        }

        public int ImageId { get; set; }
        public string FileName { get; set; } = null!;
        public byte[] Data { get; set; } = null!;

        public virtual ICollection<Menu> Menus { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Restaurant> Restaurants { get; set; }
    }
}

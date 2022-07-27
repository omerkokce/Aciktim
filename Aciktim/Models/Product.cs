using System;
using System.Collections.Generic;

namespace Aciktim.Models
{
    public partial class Product
    {
        public Product()
        {
            BasketProducts = new HashSet<BasketProduct>();
            OrderProducts = new HashSet<OrderProduct>();
            ProductCategories = new HashSet<ProductCategory>();
            ProductMenus = new HashSet<ProductMenu>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Content { get; set; }
        public int RestaurantId { get; set; }
        public int? DiscountId { get; set; }
        public int? ImageId { get; set; }

        public virtual Discount? Discount { get; set; }
        public virtual Image? Image { get; set; }
        public virtual Restaurant Restaurant { get; set; } = null!;
        public virtual ICollection<BasketProduct> BasketProducts { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        public virtual ICollection<ProductMenu> ProductMenus { get; set; }
    }
}

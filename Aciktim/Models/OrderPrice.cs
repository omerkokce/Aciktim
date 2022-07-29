using System.ComponentModel.DataAnnotations;

namespace Aciktim.Models
{
    public class OrderPrice
    {
        [Key]
        public int OrderId { get; set; }
        public decimal Price { get; set; }
    }
}

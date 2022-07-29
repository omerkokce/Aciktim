using System.ComponentModel.DataAnnotations;

namespace Aciktim.Models
{
    public class GetAddress
    {
        [Key]
        public int AddressId { get; set; }
        public string? AddressName { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Neighbourhood { get; set; }
        public string? Street { get; set; }
        public string? Apartment { get; set; }
        public string? No { get; set; }
    }
}

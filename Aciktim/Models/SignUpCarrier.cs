namespace Aciktim.Models
{
    public class SignUpCarrier
    {
        public string AddressName { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public int NeighbourhoodId { get; set; }
        public int StreetId { get; set; }
        public int ApartmentId { get; set; }
        public int ApartmentNumberId { get; set; }

        public string CarrierName { get; set; }
        public string EMail { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}

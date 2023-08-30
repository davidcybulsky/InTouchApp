namespace InTouchApi.Application.Models
{
    public class AddressDto
    {
        public int LocalNumber { get; set; }
        public int BuildingNumber { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
    }
}

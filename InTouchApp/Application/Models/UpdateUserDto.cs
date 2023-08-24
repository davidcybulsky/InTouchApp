namespace InTouchApi.Application.Models
{
    public class UpdateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }

        public UpdateAddressDto Address { get; set; }
    }
}

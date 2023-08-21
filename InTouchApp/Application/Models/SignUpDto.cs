namespace InTouchApi.Application.Models
{
    public class SignUpDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }

        public string Role { get; set; }

        public CreateAddressDto Address { get; set; }
    }
}

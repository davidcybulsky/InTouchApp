namespace InTouchApi.Application.Models
{
    public class UpdateAccountDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }

        public string FacebookURL { get; set; }
        public string InstagramURL { get; set; }
        public string LinkedInURL { get; set; }
        public string TikTokURL { get; set; }
        public string YouTubeURL { get; set; }
        public string TwitterURL { get; set; }

        public UpdateAddressDto Address { get; set; }
    }
}

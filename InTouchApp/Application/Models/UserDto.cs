namespace InTouchApi.Application.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }

        public string Role { get; set; }

        public string FacebookURL { get; set; }
        public string InstagramURL { get; set; }
        public string LinkedInURL { get; set; }
        public string TikTokURL { get; set; }
        public string YouTubeURL { get; set; }
        public string TwitterURL { get; set; }

        public AddressDto Address { get; set; }
        public IEnumerable<PostDto> Posts { get; set; }
        public IEnumerable<IncludeUserPhotoDto> UserPhotos { get; set; }
    }
}

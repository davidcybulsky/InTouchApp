namespace InTouchApi.Application.Models
{
    public class IncludeUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public IncludeUserPhotoDto UserPhoto { get; set; }
    }
}

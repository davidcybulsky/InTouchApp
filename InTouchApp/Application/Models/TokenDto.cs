namespace InTouchApi.Application.Models
{
    public class TokenDto
    {
        public required string Token { get; set; }
        public required DateTime Expires { get; set; }
    }
}

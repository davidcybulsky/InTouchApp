namespace InTouchApi.Application.Models
{
    public class TokenDto
    {
        public required int UserId { get; set; }
        public required string Token { get; set; }
        public required DateTime Created { get; set; }
        public required DateTime Expires { get; set; }

        public override string ToString()
        {
            return $" UserID: {UserId}, Token: {Token}, Created {Created}, Expires: {Expires}";
        }
    }
}

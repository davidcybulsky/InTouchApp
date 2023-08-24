namespace InTouchApi.Application.Models
{
    public class IncludeReactionDto
    {
        public int Id { get; set; }
        public string ReactionType { get; set; }
        public int UserId { get; set; }
        public IncludeUserDto Author { get; set; }
    }
}

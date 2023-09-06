namespace InTouchApi.Application.Models
{
    public class IncludeCommentDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public IncludeUserDto Author { get; set; }
        public string Content { get; set; }
        public IEnumerable<IncludeReactionDto> CommentReactions { get; set; }
    }
}

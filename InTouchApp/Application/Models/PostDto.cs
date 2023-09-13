namespace InTouchApi.Application.Models
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public IncludeUserDto Author { get; set; }
        public IEnumerable<IncludeCommentDto> Comments { get; set; }
        public IncludeReactionDto ReactionsData { get; set; } = new();
    }
}

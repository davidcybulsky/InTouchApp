namespace InTouchApi.Domain.Entities
{
    public class PostComment : Comment
    {
        public int PostId { get; set; }
        public virtual IEnumerable<CommentReaction> CommentReactions { get; set; } = new List<CommentReaction>();
    }
}

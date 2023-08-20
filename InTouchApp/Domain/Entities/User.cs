namespace InTouchApi.Domain.Entities
{
    public class User : BaseAuditableEntity
    {
        public virtual Address Address { get; set; }
        public virtual IEnumerable<Post> Posts { get; set; } = new List<Post>();
        public virtual IEnumerable<Friendship> Friends { get; set; } = new List<Friendship>();
        public virtual IEnumerable<PostComment> Comments { get; set; } = new List<PostComment>();
        public virtual IEnumerable<PostReaction> PostReactions { get; set; } = new List<PostReaction>();
        public virtual IEnumerable<CommentReaction> CommentReactions { get; set; } = new List<CommentReaction>();
    }
}

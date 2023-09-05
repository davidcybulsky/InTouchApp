using System.ComponentModel.DataAnnotations;

namespace InTouchApi.Domain.Entities
{
    public abstract class Comment : BaseAuditableEntity
    {
        public int UserId { get; set; }
        public virtual User Author { get; set; }

        [Required]
        public string Content { get; set; }
        public virtual IEnumerable<CommentReaction> CommentReactions { get; set; } = new List<CommentReaction>();
    }
}

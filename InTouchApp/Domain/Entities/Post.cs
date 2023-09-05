using System.ComponentModel.DataAnnotations;

namespace InTouchApi.Domain.Entities
{
    public class Post : BaseAuditableEntity
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public virtual User Author { get; set; }
        public virtual IEnumerable<PostComment> Comments { get; set; } = new List<PostComment>();
        public virtual IEnumerable<PostReaction> Reactions { get; set; } = new List<PostReaction>();
    }
}

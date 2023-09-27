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

        public override int GetHashCode()
        {
            return this.Id.GetHashCode() * 13 +
                   this.Title.GetHashCode() * 17 +
                   this.Content.GetHashCode() * 19 +
                   this.AuthorId.GetHashCode() * 23;
        }


        public override bool Equals(object? obj)
        {
            if (obj is not Post || obj is null)
            {
                return false;
            }
            var o = obj as Post;
            return o.Id == this.Id;
        }
    }
}

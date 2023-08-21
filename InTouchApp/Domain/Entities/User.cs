using InTouchApi.Domain.Constants;

namespace InTouchApi.Domain.Entities
{
    public class User : BaseAuditableEntity
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; } = string.Empty;

        public string Role { get; set; } = ROLES.USER;

        public string? FacebookURL { get; set; }
        public string? InstagramURL { get; set; }
        public string? LinkedInURL { get; set; }
        public string? TikTokURL { get; set; }
        public string? YouTubeURL { get; set; }
        public string? TwitterURL { get; set; }

        public virtual Address Address { get; set; }
        public virtual IEnumerable<Post> Posts { get; set; } = new List<Post>();
        public virtual IEnumerable<Friendship> Friends { get; set; } = new List<Friendship>();
        public virtual IEnumerable<PostComment> Comments { get; set; } = new List<PostComment>();
        public virtual IEnumerable<PostReaction> PostReactions { get; set; } = new List<PostReaction>();
        public virtual IEnumerable<CommentReaction> CommentReactions { get; set; } = new List<CommentReaction>();
    }
}

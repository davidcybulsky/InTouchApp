using InTouchApi.Domain.Constants;

namespace InTouchApi.Domain.Entities
{
    public abstract class Reaction : BaseAuditableEntity
    {
        public string ReactionType { get; set; } = REACTIONS.LIKE;
        public int UserId { get; set; }
    }
}

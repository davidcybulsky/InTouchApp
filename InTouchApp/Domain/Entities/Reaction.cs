namespace InTouchApi.Domain.Entities
{
    public abstract class Reaction : BaseAuditableEntity
    {
        public string ReactionType { get; set; }
        public int UserId { get; set; }
        public virtual User Author { get; set; }
    }
}

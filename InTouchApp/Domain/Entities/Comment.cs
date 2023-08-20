namespace InTouchApi.Domain.Entities
{
    public abstract class Comment : BaseAuditableEntity
    {
        public int UserId { get; set; }
        public virtual User Author { get; set; }
        public string Content { get; set; }
    }
}

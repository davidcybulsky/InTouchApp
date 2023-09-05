namespace InTouchApi.Domain.Entities
{
    public abstract class BaseAuditableEntity : BaseEntity
    {
        public DateTime CreationDate { get; set; }

        public int? CreatedById { get; set; }

        public DateTime? LastModificationDate { get; set; }

        public int? LastModifiedById { get; set; }
    }
}

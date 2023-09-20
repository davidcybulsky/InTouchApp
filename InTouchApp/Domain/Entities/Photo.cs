namespace InTouchApi.Domain.Entities
{
    public abstract class Photo : BaseAuditableEntity
    {
        public string Url { get; set; }
        public string publicPhotoId { get; set; }
    }
}

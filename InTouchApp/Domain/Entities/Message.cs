namespace InTouchApi.Domain.Entities
{
    public class Message : BaseAuditableEntity
    {
        public int SenderId { get; set; }
        public User Sender { get; set; }
        public int RecipientId { get; set; }
        public User Recipient { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
    }
}

namespace InTouchApi.Application.Models
{
    public class MessageDto
    {
        public int Id { get; set; }
        public IncludeUserDto Sender { get; set; }
        public IncludeUserDto Recipient { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
    }
}

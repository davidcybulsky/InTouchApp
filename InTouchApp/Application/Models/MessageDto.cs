namespace InTouchApi.Application.Models
{
    public class MessageDto
    {
        public IncludeUserDto Sender { get; set; }
        public IncludeUserDto Recipient { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
    }
}

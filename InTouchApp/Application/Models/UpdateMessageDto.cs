namespace InTouchApi.Application.Models
{
    public class UpdateMessageDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public string Content { get; set; }
    }
}

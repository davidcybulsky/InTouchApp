namespace InTouchApi.Domain.Entities
{
    public class UserPhoto : Photo
    {
        public int UserId { get; set; }
        public bool IsMain { get; set; }
    }
}

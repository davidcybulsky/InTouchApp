namespace InTouchApi.Domain.Entities
{
    public class Friendship : BaseAuditableEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int FriendId { get; set; }
        public virtual User Friend { get; set; }
    }
}

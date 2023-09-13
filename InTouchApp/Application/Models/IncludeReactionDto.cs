namespace InTouchApi.Application.Models
{
    public class IncludeReactionDto
    {
        public bool DidIReacted { get; set; }
        public string ReactionType { get; set; }
        public int AmountOfLikes { get; set; }
        public int AmountOfDislikes { get; set; }
    }
}

namespace InTouchApi.Application.Models
{
    public class IncludePhotoDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string publicPhotoId { get; set; }
        public bool IsMain { get; set; }
    }
}

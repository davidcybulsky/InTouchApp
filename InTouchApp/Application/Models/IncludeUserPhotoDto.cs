namespace InTouchApi.Application.Models
{
    public class IncludeUserPhotoDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string publicPhotoId { get; set; }
        public bool IsMain { get; set; }
    }
}

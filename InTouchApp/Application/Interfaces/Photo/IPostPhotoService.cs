using InTouchApi.Application.Models;

namespace InTouchApi.Application.Interfaces
{
    public interface IPostPhotoService
    {
        Task<IncludePhotoDto> AddPostPhotoAsync(int postId, IFormFile file);
        Task DeletePostPhotoAsync(int photoId);
    }
}

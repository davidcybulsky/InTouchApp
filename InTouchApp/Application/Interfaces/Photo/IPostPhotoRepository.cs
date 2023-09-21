using InTouchApi.Domain.Entities;

namespace InTouchApi.Application.Interfaces
{
    public interface IPostPhotoRepository
    {
        Task<PostPhoto> GetPostPhotoAsync(int photoId);
        Task<PostPhoto> AddPostPhotoAsync(int userId, int postId, IFormFile file);
        Task DeletePostPhotoAsync(int photoId, int userId);
    }
}

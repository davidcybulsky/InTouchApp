using InTouchApi.Domain.Entities;

namespace InTouchApi.Application.Interfaces
{
    public interface IUserPhotoRepository
    {
        Task<UserPhoto> GetUserPhotoAsync(int photoId);
        Task<UserPhoto> AddUserPhotoAsync(int userId, IFormFile file);
        Task SetAsMainUserPhotoAsync(int photoId, int userId);
        Task DeleteUserPhotoAsync(int photoId, int userId);
    }
}

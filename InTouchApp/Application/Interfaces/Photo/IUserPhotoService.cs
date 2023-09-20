using InTouchApi.Application.Models;

namespace InTouchApi.Application.Interfaces
{
    public interface IUserPhotoService
    {
        Task<UserPhotoDto> AddUserPhotoAsync(IFormFile file);
        Task SendAsMainUserPhotoAsync(int photoId);
        Task DeleteUserPhotoAsync(int photoId);
    }
}

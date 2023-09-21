using InTouchApi.Application.Models;

namespace InTouchApi.Application.Interfaces
{
    public interface IUserPhotoService
    {
        Task<IncludePhotoDto> AddUserPhotoAsync(IFormFile file);
        Task SendAsMainUserPhotoAsync(int photoId);
        Task DeleteUserPhotoAsync(int photoId);
    }
}

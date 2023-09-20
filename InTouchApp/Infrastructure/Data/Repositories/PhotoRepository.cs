using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using InTouchApi.Application.Exceptions;
using InTouchApi.Application.Interfaces;
using InTouchApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InTouchApi.Infrastructure.Data.Repositories
{
    public class PhotoRepository : IUserPhotoRepository
    {
        private readonly ApiContext _apiContext;
        private readonly Cloudinary _cloudinary;

        public PhotoRepository(ApiContext apiContext, IConfiguration configuration)
        {
            _apiContext = apiContext;

            var account = new Account
            {
                Cloud = configuration["Cloudinary:CloudName"],
                ApiKey = configuration["Cloudinary:ApiKey"],
                ApiSecret = configuration["Cloudinary:ApiSecret"]
            };

            _cloudinary = new Cloudinary(account);
        }


        public async Task<UserPhoto> AddUserPhotoAsync(int userId, IFormFile file)
        {
            var user = await _apiContext.Users
                .Where(u => u.IsDeleted == false)
                .Include(u => u.UserPhotos)
                .FirstOrDefaultAsync(u => u.Id == userId)
                ?? throw new NotFoundException("", "");

            var result = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation()
                    .Height(300)
                    .Width(300)
                    .Crop("fill"),
                    Folder = "inTouch/Photo/User"
                };
                result = await _cloudinary.UploadAsync(uploadParams);
            }

            if (result.Error != null)
            {
                throw new BadRequestException(result.Error.Message, result.Error.Message);
            }

            var photo = new UserPhoto
            {
                Url = result.SecureUrl.AbsoluteUri,
                publicPhotoId = result.PublicId,
                UserId = userId,
                CreatedById = userId,
                CreationDate = DateTime.UtcNow
            };

            if (user.UserPhotos.Count() == 0)
            {
                photo.IsMain = true;
            }

            await _apiContext.AddAsync(photo);
            await _apiContext.SaveChangesAsync();

            photo = await _apiContext.UsersPhoto.FirstOrDefaultAsync(u => u.Id == photo.Id);

            return photo;
        }

        public async Task DeleteUserPhotoAsync(int photoId, int userId)
        {
            var photo = await _apiContext.UsersPhoto
                .FirstOrDefaultAsync(u => u.Id == photoId)
                ?? throw new NotFoundException("", "");

            photo.IsDeleted = true;
            photo.LastModifiedById = userId;
            photo.LastModificationDate = DateTime.UtcNow;

            await this.SendRandomAsMain(userId);

            await _apiContext.SaveChangesAsync();

        }

        public async Task<UserPhoto> GetUserPhotoAsync(int photoId)
        {
            var photo = await _apiContext.UsersPhoto
                .AsNoTracking()
                .Where(u => u.IsDeleted == false)
                .FirstOrDefaultAsync(u => u.Id == photoId)
                ?? throw new NotFoundException("", "");

            return photo;
        }

        public async Task SetAsMainUserPhotoAsync(int photoId, int userId)
        {
            var user = await _apiContext.Users
                .Where(u => u.IsDeleted == false)
                .Include(u => u.UserPhotos)
                .FirstOrDefaultAsync(u => u.Id == userId)
                ?? throw new NotFoundException("", "");

            foreach (var userPhoto in user.UserPhotos)
            {
                userPhoto.IsMain = false;

                if (userPhoto.Id == photoId)
                {
                    userPhoto.IsMain = true;
                }
            }
        }

        public async Task SendRandomAsMain(int userId)
        {
            var user = await _apiContext.Users
                .Where(u => u.IsDeleted == false)
                .Include(u => u.UserPhotos)
                .FirstOrDefaultAsync(u => u.Id == userId)
                ?? throw new NotFoundException("", "");

            if (user.UserPhotos.Count() > 0)
            {
                user.UserPhotos.ElementAt(0).IsMain = true;
            }

            await _apiContext.SaveChangesAsync();
        }
    }
}

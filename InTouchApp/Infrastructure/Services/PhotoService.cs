using AutoMapper;
using InTouchApi.Application.Exceptions;
using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;

namespace InTouchApi.Infrastructure.Services
{
    public class PhotoService : IUserPhotoService
    {
        private readonly IMapper _mapper;
        private readonly IUserPhotoRepository _userPhotoRepository;
        private readonly IUserHttpContextService _userHttpContextService;

        public PhotoService(IUserPhotoRepository userphotorepository,
                            IUserHttpContextService userHttpContextService,
                            IMapper mapper)
        {
            _mapper = mapper;
            _userPhotoRepository = userphotorepository;
            _userHttpContextService = userHttpContextService;
        }

        public async Task<IncludePhotoDto> AddUserPhotoAsync(IFormFile file)
        {
            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized", "Unauthorized user tried to add a photo");

            var photo = await _userPhotoRepository.AddUserPhotoAsync(userId, file);

            var photoDto = _mapper.Map<IncludePhotoDto>(photo);

            return photoDto;
        }

        public async Task DeleteUserPhotoAsync(int photoId)
        {
            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized", $"Unauthorized user tried to delete the photo with id: {photoId}");

            await _userPhotoRepository.DeleteUserPhotoAsync(photoId, userId);
        }

        public async Task SendAsMainUserPhotoAsync(int photoId)
        {
            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized", $"Unauthorized user tried to sent as a main photo the photo with id: {photoId}");

            await _userPhotoRepository.SetAsMainUserPhotoAsync(photoId, userId);
        }
    }
}

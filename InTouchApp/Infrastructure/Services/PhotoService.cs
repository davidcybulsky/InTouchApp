using AutoMapper;
using InTouchApi.Application.Exceptions;
using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;

namespace InTouchApi.Infrastructure.Services
{
    public class PhotoService : IUserPhotoService
    {
        private readonly IMapper _mapper;
        private readonly IUserPhotoRepository _repository;
        private readonly IUserHttpContextService _userHttpContextService;

        public PhotoService(IUserPhotoRepository repository,
                            IUserHttpContextService userHttpContextService,
                            IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _userHttpContextService = userHttpContextService;

        }

        public async Task<UserPhotoDto> AddUserPhotoAsync(IFormFile file)
        {
            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("", "");

            var photo = await _repository.AddUserPhotoAsync(userId, file);

            var photoDto = _mapper.Map<UserPhotoDto>(photo);

            return photoDto;
        }

        public async Task DeleteUserPhotoAsync(int photoId)
        {
            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("", "");

            await _repository.DeleteUserPhotoAsync(photoId, userId);
        }

        public async Task SendAsMainUserPhotoAsync(int photoId)
        {
            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("", "");

            await _repository.SetAsMainUserPhotoAsync(photoId, userId);
        }
    }
}

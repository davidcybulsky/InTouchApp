using AutoMapper;
using InTouchApi.Application.Exceptions;
using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;

namespace InTouchApi.Infrastructure.Services
{
    public class PhotoService : IUserPhotoService, IPostPhotoService
    {
        private readonly IMapper _mapper;
        private readonly IUserPhotoRepository _userPhotoRepository;
        private readonly IUserHttpContextService _userHttpContextService;
        private readonly IPostPhotoRepository _postPhotoRepository;

        public PhotoService(IPostPhotoRepository postPhotoRepository,
                            IUserPhotoRepository userphotorepository,
                            IUserHttpContextService userHttpContextService,
                            IMapper mapper)
        {
            _mapper = mapper;
            _userPhotoRepository = userphotorepository;
            _userHttpContextService = userHttpContextService;
            _postPhotoRepository = postPhotoRepository;

        }

        public async Task<IncludePhotoDto> AddPostPhotoAsync(int postId, IFormFile file)
        {

            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("", "");

            var photo = await _postPhotoRepository.AddPostPhotoAsync(userId, postId, file);

            var photoDto = _mapper.Map<IncludePhotoDto>(photo);

            return photoDto;
        }

        public async Task<IncludePhotoDto> AddUserPhotoAsync(IFormFile file)
        {
            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("", "");

            var photo = await _userPhotoRepository.AddUserPhotoAsync(userId, file);

            var photoDto = _mapper.Map<IncludePhotoDto>(photo);

            return photoDto;
        }

        public async Task DeletePostPhotoAsync(int photoId)
        {
            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("", "");

            await _postPhotoRepository.DeletePostPhotoAsync(photoId, userId);
        }

        public async Task DeleteUserPhotoAsync(int photoId)
        {
            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("", "");

            await _userPhotoRepository.DeleteUserPhotoAsync(photoId, userId);
        }

        public async Task SendAsMainUserPhotoAsync(int photoId)
        {
            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("", "");

            await _userPhotoRepository.SetAsMainUserPhotoAsync(photoId, userId);
        }
    }
}

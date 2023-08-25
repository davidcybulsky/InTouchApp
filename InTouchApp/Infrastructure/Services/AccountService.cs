using AutoMapper;
using InTouchApi.Application.Exceptions;
using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;

namespace InTouchApi.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;
        private readonly IUserHttpContextService _userHttpContextService;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository repository,
                              IUserHttpContextService userHttpContextService,
                              IMapper mapper)
        {
            _repository = repository;
            _userHttpContextService = userHttpContextService;
            _mapper = mapper;
        }

        public async Task DeleteAccountAsync()
        {
            var id = _userHttpContextService.Id ?? throw new UnauthorizedException("");
            var account = await _repository.GetAccountAsTrackingAsync(id);
            account.IsDeleted = true;
            await _repository.UpdateAccountAsync();
        }

        public async Task<AccountDto> GetAccountAsync()
        {
            var id = _userHttpContextService.Id ?? throw new UnauthorizedException("");
            var account = await _repository.GetAccountAsync(id);
            var accountDto = _mapper.Map<AccountDto>(account);
            return accountDto;
        }

        public async Task UpdateAccountAsync(UpdateAccountDto updateAccountDto)
        {
            var id = _userHttpContextService.Id ?? throw new UnauthorizedException("");
            var account = await _repository.GetAccountAsTrackingAsync(id);
            account.FirstName = updateAccountDto.FirstName;
            account.LastName = updateAccountDto.LastName;
            account.PhoneNumber = updateAccountDto.PhoneNumber;
            account.Description = updateAccountDto.Description;

            account.FacebookURL = updateAccountDto.FacebookURL;
            account.InstagramURL = updateAccountDto.InstagramURL;
            account.LinkedInURL = updateAccountDto.LinkedInURL;
            account.TikTokURL = updateAccountDto.TikTokURL;
            account.YouTubeURL = updateAccountDto.YouTubeURL;
            account.TwitterURL = updateAccountDto.TwitterURL;

            account.Address.LocalNumber = updateAccountDto.Address.LocalNumber;
            account.Address.BuildingNumber = updateAccountDto.Address.BuildingNumber;
            account.Address.Street = updateAccountDto.Address.Street;
            account.Address.ZipCode = updateAccountDto.Address.ZipCode;
            account.Address.City = updateAccountDto.Address.City;
            account.Address.Region = updateAccountDto.Address.Region;
            account.Address.Country = updateAccountDto.Address.Country;

            await _repository.UpdateAccountAsync();
        }
    }
}

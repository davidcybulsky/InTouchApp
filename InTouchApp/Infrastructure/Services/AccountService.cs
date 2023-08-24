using AutoMapper;
using InTouchApi.Application.Exceptions;
using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;
using InTouchApi.Domain.Entities;

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
            var account = await _repository.GetAccountAsync(id);
            account.IsDeleted = true;
            await _repository.UpdateAccountAsync(account);
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
            var account = _mapper.Map<User>(updateAccountDto);
            var accountInDb = await _repository.GetAccountAsync(id);
            account.Id = accountInDb.Id;
            account.CreationDate = accountInDb.CreationDate;
            account.CreatedById = accountInDb.CreatedById;
            account.PasswordHash = accountInDb.PasswordHash;
            account.LastModificationDate = DateTime.UtcNow;
            account.LastModifiedById = id;
            await _repository.UpdateAccountAsync(account);
        }
    }
}

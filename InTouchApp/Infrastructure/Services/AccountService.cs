using AutoMapper;
using InTouchApi.Application.Exceptions;
using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;
using InTouchApi.Domain.Entities;
using Serilog;

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

            account.Id = id;

            await _repository.DeleteAccountAsync(account);

            Log.Information($"User with id: {id}, deleted its account");
        }

        public async Task<AccountDto> GetAccountAsync()
        {
            var id = _userHttpContextService.Id ?? throw new UnauthorizedException("");
            var account = await _repository.GetAccountAsync(id);

            var accountDto = _mapper.Map<AccountDto>(account);

            Log.Information($"User with id: {id}, got its account");

            return accountDto;
        }

        public async Task UpdateAccountAsync(UpdateAccountDto updateAccountDto)
        {
            var id = _userHttpContextService.Id ?? throw new UnauthorizedException("");

            var account = _mapper.Map<User>(updateAccountDto);

            account.Id = id;

            await _repository.UpdateAccountAsync(account);

            Log.Information($"User with id: {id}, updated its account");
        }
    }
}

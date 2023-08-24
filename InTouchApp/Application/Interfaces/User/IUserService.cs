using InTouchApi.Application.Models;

namespace InTouchApi.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(int id);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<int> CreateUserAsync(CreateUserDto createUserDto);
        Task UpdateUserAsync(int id, UpdateUserDto updateUserDto);
        Task UpdateUserRoleAsync(int id, UpdateRoleDto updateRoleDto);
        Task DeleteUserAsync(int id);
    }
}

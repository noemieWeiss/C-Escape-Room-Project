using EscapeRoom.Core.DTOs;

namespace EscapeRoom.Core.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(int id);
        Task<UserDto> CreateAsync(UserDto userDto);
        Task<UserDto> UpdateAsync(int id, UserDto userDto);
        Task DeleteAsync(int id);
    }
}

using EscapeRoom.Core.DTOs;

namespace EscapeRoom.Core.Services
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDto>> GetAllAsync();
        Task<RoomDto> GetByIdAsync(int id);
        Task<RoomDto> CreateAsync(RoomDto roomDto);
        Task<RoomDto> UpdateAsync(int id, RoomDto roomDto);
        Task DeleteAsync(int id);
    }
}

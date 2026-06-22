using EscapeRoom.Core.DTOs;

namespace EscapeRoom.Core.Services
{
    public interface IDifficultyLevelService
    {
        Task<IEnumerable<DifficultyLevelDto>> GetAllAsync();
        Task<DifficultyLevelDto> GetByIdAsync(int id);
        Task<DifficultyLevelDto> CreateAsync(DifficultyLevelDto dto);
        Task<DifficultyLevelDto> UpdateAsync(int id, DifficultyLevelDto dto);
        Task DeleteAsync(int id);
    }
}

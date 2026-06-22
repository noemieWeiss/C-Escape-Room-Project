using EscapeRoom.Core.DTOs;

namespace EscapeRoom.Core.Services
{
    public interface IHintService
    {
        Task<IEnumerable<HintDto>> GetAllAsync();
        Task<HintDto> GetByIdAsync(int id);
        Task<HintDto> CreateAsync(HintDto hintDto);
        Task<HintDto> UpdateAsync(int id, HintDto hintDto);
        Task DeleteAsync(int id);
    }
}

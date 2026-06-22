using AutoMapper;
using EscapeRoom.Core.DTOs;
using EscapeRoom.Core.Exceptions;
using EscapeRoom.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace EscapeRoom.Data.Services
{
    public class DifficultyLevelService : IDifficultyLevelService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DifficultyLevelService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DifficultyLevelDto>> GetAllAsync()
        {
            var levels = await _context.DifficultyLevels.ToListAsync();
            return _mapper.Map<IEnumerable<DifficultyLevelDto>>(levels);
        }

        public async Task<DifficultyLevelDto> GetByIdAsync(int id)
        {
            var level = await _context.DifficultyLevels.FindAsync(id);
            if (level == null)
                throw new NotFoundException($"Difficulty level with id {id} was not found.");

            return _mapper.Map<DifficultyLevelDto>(level);
        }

        public async Task<DifficultyLevelDto> CreateAsync(DifficultyLevelDto dto)
        {
            await ValidateUniqueNameAsync(dto.Name);

            var level = _mapper.Map<Core.Entities.DifficultyLevel>(dto);
            _context.DifficultyLevels.Add(level);
            await _context.SaveChangesAsync();

            return _mapper.Map<DifficultyLevelDto>(level);
        }

        public async Task<DifficultyLevelDto> UpdateAsync(int id, DifficultyLevelDto dto)
        {
            var level = await _context.DifficultyLevels.FindAsync(id);
            if (level == null)
                throw new NotFoundException($"Difficulty level with id {id} was not found.");

            await ValidateUniqueNameAsync(dto.Name, id);

            level.Name = dto.Name;
            await _context.SaveChangesAsync();

            return _mapper.Map<DifficultyLevelDto>(level);
        }

        public async Task DeleteAsync(int id)
        {
            var level = await _context.DifficultyLevels.FindAsync(id);
            if (level == null)
                throw new NotFoundException($"Difficulty level with id {id} was not found.");

            var inUse = await _context.EscapeRooms.AnyAsync(r => r.DifficultyLevelId == id);
            if (inUse)
                throw new BadRequestException("Cannot delete a difficulty level that is assigned to rooms.");

            _context.DifficultyLevels.Remove(level);
            await _context.SaveChangesAsync();
        }

        private async Task ValidateUniqueNameAsync(string name, int? excludeId = null)
        {
            var exists = await _context.DifficultyLevels
                .AnyAsync(d => d.Name == name && (!excludeId.HasValue || d.Id != excludeId.Value));

            if (exists)
                throw new BadRequestException($"Difficulty level '{name}' already exists.");
        }
    }
}

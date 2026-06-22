using AutoMapper;
using EscapeRoom.Core.DTOs;
using EscapeRoom.Core.Exceptions;
using EscapeRoom.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace EscapeRoom.Data.Services
{
    public class HintService : IHintService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public HintService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HintDto>> GetAllAsync()
        {
            var hints = await _context.Hints.ToListAsync();
            return _mapper.Map<IEnumerable<HintDto>>(hints);
        }

        public async Task<HintDto> GetByIdAsync(int id)
        {
            var hint = await _context.Hints.FindAsync(id);
            if (hint == null)
                throw new NotFoundException($"Hint with id {id} was not found.");

            return _mapper.Map<HintDto>(hint);
        }

        public async Task<HintDto> CreateAsync(HintDto hintDto)
        {
            await ValidateRoomExistsAsync(hintDto.EscapeRoomId);

            var hint = _mapper.Map<Core.Entities.Hint>(hintDto);
            _context.Hints.Add(hint);
            await _context.SaveChangesAsync();

            return _mapper.Map<HintDto>(hint);
        }

        public async Task<HintDto> UpdateAsync(int id, HintDto hintDto)
        {
            var hint = await _context.Hints.FindAsync(id);
            if (hint == null)
                throw new NotFoundException($"Hint with id {id} was not found.");

            await ValidateRoomExistsAsync(hintDto.EscapeRoomId);

            hint.Text = hintDto.Text;
            hint.EscapeRoomId = hintDto.EscapeRoomId;

            await _context.SaveChangesAsync();
            return _mapper.Map<HintDto>(hint);
        }

        public async Task DeleteAsync(int id)
        {
            var hint = await _context.Hints.FindAsync(id);
            if (hint == null)
                throw new NotFoundException($"Hint with id {id} was not found.");

            _context.Hints.Remove(hint);
            await _context.SaveChangesAsync();
        }

        private async Task ValidateRoomExistsAsync(int escapeRoomId)
        {
            var exists = await _context.EscapeRooms.AnyAsync(r => r.Id == escapeRoomId);
            if (!exists)
                throw new BadRequestException($"Room with id {escapeRoomId} was not found.");
        }
    }
}

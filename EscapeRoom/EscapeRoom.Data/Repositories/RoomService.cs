using AutoMapper;
using EscapeRoom.Core.DTOs;
using EscapeRoom.Core.Entities;
using EscapeRoom.Core.Exceptions;
using EscapeRoom.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace EscapeRoom.Data.Repositories
{
    public class RoomService : IRoomService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RoomService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoomDto>> GetAllAsync()
        {
            var rooms = await _context.EscapeRooms
                .Include(r => r.DifficultyLevel)
                .ToListAsync();

            return _mapper.Map<IEnumerable<RoomDto>>(rooms);
        }

        public async Task<RoomDto> GetByIdAsync(int id)
        {
            var room = await _context.EscapeRooms
                .Include(r => r.DifficultyLevel)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (room == null)
                throw new NotFoundException($"Room with id {id} was not found.");

            return _mapper.Map<RoomDto>(room);
        }

        public async Task<RoomDto> CreateAsync(RoomDto roomDto)
        {
            var difficulty = await ResolveDifficultyAsync(roomDto.Difficulty);

            var room = _mapper.Map<EscapeRoomEntity>(roomDto);
            room.DifficultyLevelId = difficulty.Id;

            _context.EscapeRooms.Add(room);
            await _context.SaveChangesAsync();

            await _context.Entry(room).Reference(r => r.DifficultyLevel).LoadAsync();

            return _mapper.Map<RoomDto>(room);
        }

        public async Task<RoomDto> UpdateAsync(int id, RoomDto roomDto)
        {
            var room = await _context.EscapeRooms
                .Include(r => r.DifficultyLevel)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (room == null)
                throw new NotFoundException($"Room with id {id} was not found.");

            var difficulty = await ResolveDifficultyAsync(roomDto.Difficulty);

            room.Name = roomDto.Name;
            room.MaxParticipants = roomDto.MaxCapacity;
            room.DifficultyLevelId = difficulty.Id;

            await _context.SaveChangesAsync();
            await _context.Entry(room).Reference(r => r.DifficultyLevel).LoadAsync();

            return _mapper.Map<RoomDto>(room);
        }

        public async Task DeleteAsync(int id)
        {
            var room = await _context.EscapeRooms.FindAsync(id);

            if (room == null)
                throw new NotFoundException($"Room with id {id} was not found.");

            _context.EscapeRooms.Remove(room);
            await _context.SaveChangesAsync();
        }

        private async Task<DifficultyLevel> ResolveDifficultyAsync(string difficultyName)
        {
            var difficulty = await _context.DifficultyLevels
                .FirstOrDefaultAsync(d => d.Name == difficultyName);

            if (difficulty == null)
                throw new BadRequestException($"Difficulty level '{difficultyName}' was not found.");

            return difficulty;
        }
    }
}

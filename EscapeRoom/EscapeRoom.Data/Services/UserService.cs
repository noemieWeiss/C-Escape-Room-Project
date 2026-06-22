using AutoMapper;
using EscapeRoom.Core.DTOs;
using EscapeRoom.Core.Exceptions;
using EscapeRoom.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace EscapeRoom.Data.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var players = await _context.Players.ToListAsync();
            return _mapper.Map<IEnumerable<UserDto>>(players);
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
                throw new NotFoundException($"User with id {id} was not found.");

            return _mapper.Map<UserDto>(player);
        }

        public async Task<UserDto> CreateAsync(UserDto userDto)
        {
            await ValidateUniqueEmailAsync(userDto.Email);

            var player = _mapper.Map<Core.Entities.Player>(userDto);
            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(player);
        }

        public async Task<UserDto> UpdateAsync(int id, UserDto userDto)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
                throw new NotFoundException($"User with id {id} was not found.");

            await ValidateUniqueEmailAsync(userDto.Email, id);

            player.FullName = userDto.FullName;
            player.Email = userDto.Email;
            player.Phone = userDto.Phone;

            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(player);
        }

        public async Task DeleteAsync(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
                throw new NotFoundException($"User with id {id} was not found.");

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
        }

        private async Task ValidateUniqueEmailAsync(string email, int? excludeId = null)
        {
            var exists = await _context.Players
                .AnyAsync(p => p.Email == email && (!excludeId.HasValue || p.Id != excludeId.Value));

            if (exists)
                throw new BadRequestException($"A user with email '{email}' already exists.");
        }
    }
}

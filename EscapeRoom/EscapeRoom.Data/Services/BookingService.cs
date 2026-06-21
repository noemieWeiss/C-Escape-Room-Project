using AutoMapper;
using EscapeRoom.Core.DTOs;
using EscapeRoom.Core.Exceptions;
using EscapeRoom.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace EscapeRoom.Data.Services
{
    public class BookingService : IBookingService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BookingService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookingDto>> GetAllAsync()
        {
            var bookings = await _context.Bookings.ToListAsync();
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        public async Task<BookingDto> GetByIdAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                throw new NotFoundException($"Booking with id {id} was not found.");

            return _mapper.Map<BookingDto>(booking);
        }

        public async Task<BookingDto> CreateAsync(BookingDto bookingDto)
        {
            await ValidateForeignKeysAsync(bookingDto.PlayerId, bookingDto.EscapeRoomId);
            await ValidateParticipantCountAsync(bookingDto.EscapeRoomId, bookingDto.NumberOfParticipants);

            var booking = _mapper.Map<Core.Entities.Booking>(bookingDto);
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return _mapper.Map<BookingDto>(booking);
        }

        public async Task<BookingDto> UpdateAsync(int id, BookingDto bookingDto)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                throw new NotFoundException($"Booking with id {id} was not found.");

            await ValidateForeignKeysAsync(bookingDto.PlayerId, bookingDto.EscapeRoomId);
            await ValidateParticipantCountAsync(bookingDto.EscapeRoomId, bookingDto.NumberOfParticipants);

            booking.PlayerId = bookingDto.PlayerId;
            booking.EscapeRoomId = bookingDto.EscapeRoomId;
            booking.BookingDateTime = bookingDto.BookingDateTime;
            booking.NumberOfParticipants = bookingDto.NumberOfParticipants;
            booking.Status = bookingDto.Status;

            await _context.SaveChangesAsync();

            return _mapper.Map<BookingDto>(booking);
        }

        public async Task DeleteAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                throw new NotFoundException($"Booking with id {id} was not found.");

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }

        private async Task ValidateForeignKeysAsync(int playerId, int escapeRoomId)
        {
            var playerExists = await _context.Players.AnyAsync(p => p.Id == playerId);
            if (!playerExists)
                throw new BadRequestException($"Player with id {playerId} was not found.");

            var roomExists = await _context.EscapeRooms.AnyAsync(r => r.Id == escapeRoomId);
            if (!roomExists)
                throw new BadRequestException($"Room with id {escapeRoomId} was not found.");
        }

        private async Task ValidateParticipantCountAsync(int escapeRoomId, int numberOfParticipants)
        {
            var room = await _context.EscapeRooms.FindAsync(escapeRoomId);
            if (room != null && numberOfParticipants > room.MaxParticipants)
                throw new BadRequestException(
                    $"NumberOfParticipants ({numberOfParticipants}) exceeds room capacity ({room.MaxParticipants}).");
        }
    }
}

using EscapeRoom.Core.DTOs;

namespace EscapeRoom.Core.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDto>> GetAllAsync();
        Task<BookingDto> GetByIdAsync(int id);
        Task<BookingDto> CreateAsync(BookingDto bookingDto);
        Task<BookingDto> UpdateAsync(int id, BookingDto bookingDto);
        Task DeleteAsync(int id);
    }
}

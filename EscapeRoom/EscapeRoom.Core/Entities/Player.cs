using System.ComponentModel.DataAnnotations;

namespace EscapeRoom.Core.Entities
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        
        public string FullName { get; set; } = string.Empty;
        
        public string Email { get; set; } = string.Empty;
        
        public string? Phone { get; set; } // סימן שאלה אומר שהטלפון יכול להיות ריק (Null) ב-DB

        // קשר ניווט: שחקן אחד יכול לבצע הרבה הזמנות לאורך הזמן
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
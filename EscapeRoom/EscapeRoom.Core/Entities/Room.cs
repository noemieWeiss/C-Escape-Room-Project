using System.ComponentModel.DataAnnotations;

namespace EscapeRoom.Core.Entities
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Difficulty { get; set; } = string.Empty;
        public int MaxCapacity { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Hint> Hints { get; set; } = new List<Hint>();
    }
}
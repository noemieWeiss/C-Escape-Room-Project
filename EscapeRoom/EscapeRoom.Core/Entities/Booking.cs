using System.ComponentModel.DataAnnotations;

namespace EscapeRoom.Core.Entities
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        public int PlayerId { get; set; }
        public Player? Player { get; set; }

        public int EscapeRoomId { get; set; }
        public EscapeRoomEntity? EscapeRoom { get; set; }

        public DateTime BookingDateTime { get; set; }

        public int NumberOfParticipants { get; set; }

        public string Status { get; set; } = "Pending";
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EscapeRoom.Core.Entities
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        public DateTime BookingDate { get; set; }
        public int NumberOfParticipants { get; set; }

        // Foreign key to the player who booked
        public int PlayerId { get; set; }

        [ForeignKey("PlayerId")]
        public Player Player { get; set; } = null!;

        // Foreign key to the room that was booked
        public int RoomId { get; set; }

        [ForeignKey("RoomId")]
        public Room Room { get; set; } = null!;
    }
}

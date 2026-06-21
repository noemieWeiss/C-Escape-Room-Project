using System.ComponentModel.DataAnnotations;

namespace EscapeRoom.Core.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "PlayerId must be a positive value.")]
        public int PlayerId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "EscapeRoomId must be a positive value.")]
        public int EscapeRoomId { get; set; }

        [Required]
        public DateTime BookingDateTime { get; set; }

        [Required]
        [Range(1, 50, ErrorMessage = "NumberOfParticipants must be between 1 and 50.")]
        public int NumberOfParticipants { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending";
    }
}

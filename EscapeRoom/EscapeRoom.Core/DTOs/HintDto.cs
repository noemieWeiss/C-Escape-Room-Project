using System.ComponentModel.DataAnnotations;

namespace EscapeRoom.Core.DTOs
{
    public class HintDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 1)]
        public string Text { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "EscapeRoomId must be a positive value.")]
        public int EscapeRoomId { get; set; }
    }
}

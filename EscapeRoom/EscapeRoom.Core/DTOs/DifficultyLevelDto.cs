using System.ComponentModel.DataAnnotations;

namespace EscapeRoom.Core.DTOs
{
    public class DifficultyLevelDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;
    }
}

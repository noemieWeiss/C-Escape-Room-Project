using System.ComponentModel.DataAnnotations;

namespace EscapeRoom.Core.DTOs
{
    public class RoomDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Difficulty { get; set; } = string.Empty;

        [Required]
        [Range(1, 50, ErrorMessage = "MaxCapacity must be between 1 and 50.")]
        public int MaxCapacity { get; set; }

        // שימי לב: אנחנו לא חושפות פה את הרשימות של ההזמנות (Bookings) או הרמזים (Hints)!
        // המטרה היא שהלקוח (למשל אפליקציית הריאקט) יקבל רק את פרטי החדר נטו.
    }
}
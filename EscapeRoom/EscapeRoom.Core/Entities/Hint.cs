using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EscapeRoom.Core.Entities
{
    public class Hint
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;

        // התעדכן ל-RoomId
        public int RoomId { get; set; }

        [ForeignKey("RoomId")]
        public Room Room { get; set; } = null!;
    }
}
using System.ComponentModel.DataAnnotations;

namespace EscapeRoom.Core.Entities
{
    public class Hint
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;

        public int EscapeRoomId { get; set; }
        public EscapeRoomEntity? EscapeRoom { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EscapeRoom.Core.Entities
{
    public class Hint
    {
        [Key]
        public int Id { get; set; }
<<<<<<< HEAD
        public string Description { get; set; } = string.Empty;

        // התעדכן ל-RoomId
        public int RoomId { get; set; }

        [ForeignKey("RoomId")]
        public Room Room { get; set; } = null!;
=======
        public string Text { get; set; } = string.Empty;

        // מפתח זר לחדר הבריחה
        public int EscapeRoomId { get; set; }
        public EscapeRoomEntity? EscapeRoom { get; set; }
>>>>>>> e75f471 (all the cors are working and the db is conected and created at the sqlserver)
    }
}
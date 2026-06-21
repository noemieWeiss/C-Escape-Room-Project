using System.Collections.Generic;
namespace EscapeRoom.Core.Entities
{
    public class DifficultyLevel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<EscapeRoomEntity> EscapeRooms { get; set; } = new List<EscapeRoomEntity>();
    }
}
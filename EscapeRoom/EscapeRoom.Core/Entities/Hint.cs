namespace EscapeRoom.Core.Entities
{
    public class Hint
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int PenaltyMinutes { get; set; }

        public int EscapeRoomEntityId { get; set; }
        public EscapeRoomEntity EscapeRoomEntity { get; set; }
    }
}
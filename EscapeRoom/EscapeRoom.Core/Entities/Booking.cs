using System;

namespace EscapeRoom.Core.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public int ActualParticipants { get; set; }

        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int EscapeRoomEntityId { get; set; }
        public EscapeRoomEntity EscapeRoomEntity { get; set; }
    }
}
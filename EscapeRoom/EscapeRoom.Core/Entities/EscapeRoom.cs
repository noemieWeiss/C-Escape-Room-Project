using System.Collections.Generic;

namespace EscapeRoom.Core.Entities
{
    // כדי למנוע התנגשות שם עם ה-Namespace, נקרא למחלקה Room או שנוודא שה-Namespace שונה.
    // מכיוון שקראנו לפרויקט EscapeRoom.Core, עדיף לקרוא למחלקה EscapeRoomEntity או פשוט לשנות קצת.
    // נשתמש בשם EscapeRoomEntity כדי להיות בטוחים.
    public class EscapeRoomEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DifficultyLevel { get; set; }
        public int MaxParticipants { get; set; }

        public ICollection<Hint> Hints { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
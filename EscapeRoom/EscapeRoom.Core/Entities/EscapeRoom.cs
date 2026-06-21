using System.Collections.Generic;

namespace EscapeRoom.Core.Entities
{
    // כדי למנוע התנגשות שם עם ה-Namespace, נקרא למחלקה Room או שנוודא שה-Namespace שונה.
    // מכיוון שקראנו לפרויקט EscapeRoom.Core, עדיף לקרוא למחלקה EscapeRoomEntity או פשוט לשנות קצת.
    // נשתמש בשם EscapeRoomEntity כדי להיות בטוחים.
    public class EscapeRoomEntity
    {
        public int Id { get; set; }
        
        // תיקון: אתחול עם string.Empty כדי למנוע אזהרת גירסאות C# חדשות (Non-nullable property)
        public string Name { get; set; } = string.Empty;

        // תיקון: שינוי ל-DifficultyLevelId כדי שיהיה מפתח זר (FK) ברור לטבלת הקטלוג
        public int DifficultyLevelId { get; set; }
        
        // תוספת: פרופרטי ניווט המאפשר ל-EF Core לשלוף את אובייקט דרגת הקושי המלא במידת הצורך
        public DifficultyLevel? DifficultyLevel { get; set; }

        public int MaxParticipants { get; set; }

        // תיקון: אתחול האוספים כ-List חדש כדי למנוע שגיאות Null Reference בזמן ריצה
        public ICollection<Hint> Hints { get; set; } = new List<Hint>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
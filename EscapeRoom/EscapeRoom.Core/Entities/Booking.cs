using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EscapeRoom.Core.Entities
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
<<<<<<< HEAD
        public DateTime BookingDate { get; set; }
        public int NumberOfParticipants { get; set; }
=======
>>>>>>> e75f471 (all the cors are working and the db is conected and created at the sqlserver)

        // מפתח זר וקשר לשחקן שהזמין
        public int PlayerId { get; set; }
<<<<<<< HEAD
        [ForeignKey("PlayerId")]
        public Player Player { get; set; } = null!;

        // התעדכן ל-RoomId
        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public Room Room { get; set; } = null!;
=======
        public Player? Player { get; set; }

        // מפתח זר וקשר לחדר שהוזמן
        public int EscapeRoomId { get; set; }
        public EscapeRoomEntity? EscapeRoom { get; set; }

        // תאריך ושעה של המשחק
        public DateTime BookingDateTime { get; set; }
        
        // כמות המשתתפים שהוזמנו למשחק הספציפי הזה
        public int NumberOfParticipants { get; set; }

        // סטטוס ההזמנה (למשל: אושר, בוטל) - אפשר להוסיף טבלת קטלוג לסטטוסים בהמשך במידת הצורך
        public string Status { get; set; } = "Pending"; 
>>>>>>> e75f471 (all the cors are working and the db is conected and created at the sqlserver)
    }
}
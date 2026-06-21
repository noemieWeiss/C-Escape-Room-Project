namespace EscapeRoom.Core.DTOs
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Difficulty { get; set; } = string.Empty;
        public int MaxCapacity { get; set; }

        // שימי לב: אנחנו לא חושפות פה את הרשימות של ההזמנות (Bookings) או הרמזים (Hints)!
        // המטרה היא שהלקוח (למשל אפליקציית הריאקט) יקבל רק את פרטי החדר נטו.
    }
}
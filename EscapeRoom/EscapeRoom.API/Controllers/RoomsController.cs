using Microsoft.AspNetCore.Mvc;

namespace EscapeRoom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        // פונקציית GET שמחזירה את רשימת החדרים
        [HttpGet]
        public IActionResult GetAllRooms()
        {
            // נתונים זמניים כדי לבדוק שה-API שלנו עובד ומחזיר JSON
            var mockRooms = new[]
            {
                new { Id = 1, Name = "The Matrix", Difficulty = "Hard", Capacity = 4 },
                new { Id = 2, Name = "Pirate's Treasure", Difficulty = "Medium", Capacity = 6 }
            };

            // מחזיר סטטוס 200 OK יחד עם הנתונים
            return Ok(mockRooms);
        }
    }
}
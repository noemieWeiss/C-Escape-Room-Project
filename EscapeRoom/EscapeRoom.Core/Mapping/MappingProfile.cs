using AutoMapper;
using EscapeRoom.Core.DTOs;
using EscapeRoom.Core.Entities;

namespace EscapeRoom.Core.Mapping
{
    // המחלקה חייבת לרשת מ-Profile של AutoMapper
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // הגדרת המיפוי: מהישות במסד הנתונים ל-DTO (לקריאת נתונים והחזרה ללקוח)
            CreateMap<Room, RoomDto>();

            // הגדרת המיפוי ההפוך: מ-DTO לישות (אם הלקוח שולח לנו נתונים ליצירת חדר חדש)
            CreateMap<RoomDto, Room>();
        }
    }
}
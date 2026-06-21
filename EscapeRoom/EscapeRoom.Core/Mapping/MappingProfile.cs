using AutoMapper;
using EscapeRoom.Core.DTOs;
using EscapeRoom.Core.Entities;

namespace EscapeRoom.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EscapeRoomEntity, RoomDto>()
                .ForMember(dest => dest.MaxCapacity, opt => opt.MapFrom(src => src.MaxParticipants))
                .ForMember(dest => dest.Difficulty, opt => opt.MapFrom(src =>
                    src.DifficultyLevel != null ? src.DifficultyLevel.Name : string.Empty));

            CreateMap<RoomDto, EscapeRoomEntity>()
                .ForMember(dest => dest.MaxParticipants, opt => opt.MapFrom(src => src.MaxCapacity))
                .ForMember(dest => dest.DifficultyLevelId, opt => opt.Ignore())
                .ForMember(dest => dest.DifficultyLevel, opt => opt.Ignore())
                .ForMember(dest => dest.Hints, opt => opt.Ignore())
                .ForMember(dest => dest.Bookings, opt => opt.Ignore());

            CreateMap<Booking, BookingDto>();
            CreateMap<BookingDto, Booking>()
                .ForMember(dest => dest.Player, opt => opt.Ignore())
                .ForMember(dest => dest.EscapeRoom, opt => opt.Ignore());
        }
    }
}

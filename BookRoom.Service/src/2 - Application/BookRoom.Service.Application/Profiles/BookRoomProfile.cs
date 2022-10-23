using AutoMapper;
using BookRoom.Service.Domain.Contract.Notifications;
using BookRoom.Services.Domain.Entities;

namespace BookRoom.Service.Application.Profiles
{
    public class BookRoomProfile : Profile
    {
        public BookRoomProfile()
        {
            CreateMap<BookRoomsNotification, BookRooms>()
                .ForMember(x => x.Active, opt => opt.MapFrom(src => src.Active))                
                .ForMember(x => x.DatAlt, opt => opt.MapFrom(src => src.DatAlt))
                .ForMember(x => x.DatInc, opt => opt.MapFrom(src => src.DatInc))
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(x => x.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(x => x.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(x => x.User, opt => opt.MapFrom(src => src.User))
                .ForMember(x => x.Room, opt => opt.MapFrom(src => src.Room))
                .ReverseMap();
        }
    }
}

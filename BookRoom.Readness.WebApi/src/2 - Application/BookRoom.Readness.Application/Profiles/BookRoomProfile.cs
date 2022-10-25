using AutoMapper;
using BookRoom.Readness.Domain.Contract.Responses.BookRoomsResponses;
using BookRoom.Readness.Domain.Entities;

namespace BookRoom.Readness.Application.Profiles
{
    public class BookRoomProfile : Profile
    {
        public BookRoomProfile()
        {
            CreateMap<BookRoomResponse, BookRooms>()
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

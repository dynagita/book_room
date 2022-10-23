using AutoMapper;
using BookRoom.Domain.Contract.DataTransferObjects.BookRoomDtos;
using BookRoom.Domain.Contract.Notification.BookRooms;
using BookRoom.Domain.Contract.Requests.Commands.BookRooms;
using BookRoom.Domain.Contract.Responses.BookRoomsResponses;
using BookRoom.Domain.Entities;

namespace BookRoom.Application.Mappers
{
    public class BookRoomProfile : Profile
    {
        public BookRoomProfile()
        {
            CreateMap<BookRooms, CreateBookRoomRequest>()
                .ForMember(x => x.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(x => x.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(x => x.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(x => x.User, opt => opt.MapFrom(src => src.User))
                .ForMember(x => x.Room, opt => opt.MapFrom(src => src.Room))
                .ForMember(x => x.Reference, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
            CreateMap<BookRooms, UpdateBookRoomRequest>()
                .ForMember(x => x.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(x => x.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(x => x.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(x => x.User, opt => opt.MapFrom(src => src.User))
                .ForMember(x => x.Room, opt => opt.MapFrom(src => src.Room))
                .ForMember(x => x.Reference, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
            CreateMap<BookRooms, BookRoomResponse>()
                .ForMember(x => x.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(x => x.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(x => x.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(x => x.User, opt => opt.MapFrom(src => src.User))
                .ForMember(x => x.Room, opt => opt.MapFrom(src => src.Room))
                .ForMember(x => x.Reference, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
            CreateMap<BookRooms, BookRoomNotification>()
                .ForMember(x => x.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(x => x.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(x => x.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(x => x.User, opt => opt.MapFrom(src => src.User))
                .ForMember(x => x.Room, opt => opt.MapFrom(src => src.Room))
                .ForMember(x => x.Reference, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<BookRooms, BookRoomValidationDTO>()
                .ForMember(x => x.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(x => x.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(x => x.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.DatAlt, opt => opt.MapFrom(src => src.DatAlt))
                .ReverseMap();
        }
    }
}

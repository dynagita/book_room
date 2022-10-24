using AutoMapper;
using BookRoom.Domain.Contract.Requests.Commands.RoomCommands;
using BookRoom.Domain.Contract.Responses.RoomResponses;
using BookRoom.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace BookRoom.Application.Mappers
{
    [ExcludeFromCodeCoverage]
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, CreateRoomRequest>()
                .ForMember(x => x.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(x => x.Reference, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Description))
                .ReverseMap();
            CreateMap<Room, UpdateRoomRequest>()
                .ForMember(x => x.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(x => x.Reference, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Description))
                .ReverseMap();
            CreateMap<Room, RoomResponse>()
                .ForMember(x => x.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(x => x.Reference, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Description))
                .ReverseMap();
        }
    }
}

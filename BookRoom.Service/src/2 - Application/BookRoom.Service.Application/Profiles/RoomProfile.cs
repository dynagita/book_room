using AutoMapper;
using BookRoom.Service.Domain.Contract.Notifications;
using BookRoom.Services.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace BookRoom.Service.Application.Profiles
{
    [ExcludeFromCodeCoverage]
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<RoomNotification, Room>()
                .ForMember(x => x.Active, opt => opt.MapFrom(src => src.Active))
                .ForMember(x => x.Books, opt => opt.Ignore())
                .ForMember(x => x.DatAlt, opt => opt.MapFrom(src => src.DatAlt))
                .ForMember(x => x.DatInc, opt => opt.MapFrom(src => src.DatInc))
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(x => x.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(x => x.Title, opt => opt.MapFrom(src => src.Title))
                .ReverseMap();
        }
    }
}

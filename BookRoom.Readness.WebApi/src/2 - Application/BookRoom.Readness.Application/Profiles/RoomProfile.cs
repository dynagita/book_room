using AutoMapper;
using BookRoom.Readness.Domain.Contract.Responses.RoomResponses;
using BookRoom.Readness.Domain.Entities;

namespace BookRoom.Readness.Application.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<RoomResponse, Room>()                
                .ForMember(x => x.Books, opt => opt.MapFrom(src=>src.Books))
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(x => x.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(x => x.Title, opt => opt.MapFrom(src => src.Title))
                .ReverseMap();
        }
    }
}

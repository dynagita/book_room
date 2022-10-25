using AutoMapper;
using BookRoom.Readness.Domain.Contract.Responses.UserResponses;
using BookRoom.Readness.Domain.Entities;

namespace BookRoom.Readness.Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponse>()
                .ForMember(x => x.Reference, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(x => x.BornDate, opt => opt.MapFrom(src => src.BornDate))
                .ForMember(x => x.Books, opt => opt.MapFrom(src => src.Books))
                .ReverseMap();
        }
    }
}

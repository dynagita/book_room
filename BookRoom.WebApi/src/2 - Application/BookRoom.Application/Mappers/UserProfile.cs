using AutoMapper;
using BookRoom.Domain.Contract.Requests.Commands.UserCommands;
using BookRoom.Domain.Contract.Responses.UserResponses;
using BookRoom.Domain.Entities;

namespace BookRoom.Application.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserCreateRequest>()
                .ForMember(x => x.Reference, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(x => x.BornDate, opt => opt.MapFrom(src => src.BornDate))
                .ForMember(x => x.Password, opt => opt.MapFrom(src => src.Password))
                .ReverseMap();
            CreateMap<User, UserResponse>()
                .ForMember(x => x.Reference, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(x => x.BornDate, opt => opt.MapFrom(src => src.BornDate))
                .ReverseMap();
        }
    }
}

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
                .ForMember(x => x.Reference, src => src.MapFrom(opt => opt.Id))
                .ForMember(x => x.Email, src => src.MapFrom(opt => opt.Email))
                .ForMember(x => x.FirstName, src => src.MapFrom(opt => opt.Name))
                .ForMember(x => x.LastName, src => src.MapFrom(opt => opt.LastName))
                .ForMember(x => x.BornDate, src => src.MapFrom(opt => opt.BornDate))
                .ForMember(x => x.Password, src => src.MapFrom(opt => opt.Password))
                .ReverseMap();
            CreateMap<User, UserResponse>()
                .ForMember(x => x.Reference, src => src.MapFrom(opt => opt.Id))
                .ForMember(x => x.Email, src => src.MapFrom(opt => opt.Email))
                .ForMember(x => x.FirstName, src => src.MapFrom(opt => opt.Name))
                .ForMember(x => x.LastName, src => src.MapFrom(opt => opt.LastName))
                .ForMember(x => x.BornDate, src => src.MapFrom(opt => opt.BornDate))
                .ReverseMap();
        }
    }
}

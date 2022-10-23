using AutoMapper;
using BookRoom.Service.Domain.Contract.Notifications;
using BookRoom.Services.Domain.Entities;

namespace BookRoom.Service.Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserNotification, User>()
                .ForMember(x => x.Active, opt => opt.MapFrom(src => src.Active))
                .ForMember(x => x.DatAlt, opt => opt.MapFrom(src => src.DatAlt))
                .ForMember(x => x.DatInc, opt => opt.MapFrom(src => src.DatInc))
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Books, opt => opt.Ignore())
                .ForMember(x => x.BornDate, opt => opt.MapFrom(src => src.BornDate))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.Password, opt => opt.MapFrom(src => src.Password))
                .ReverseMap();
        }
    }
}

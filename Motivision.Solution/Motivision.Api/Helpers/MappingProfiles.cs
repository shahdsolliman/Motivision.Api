using AutoMapper;
using Motivision.Core.Identity.Entities;
using SnapShop.API.Dtos;
using System.Runtime.CompilerServices;

namespace SnapShop.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            CreateMap<RegisterDto, AppUser>();

            CreateMap<AppUser, UserDto>()
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

        }
    }
}

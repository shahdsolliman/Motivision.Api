using AutoMapper;
using Motivision.Api.DTOs;
using Motivision.Core.Business.Entities;
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

            CreateMap<FocusSession, FocusSessionDto>()
                .ForMember(dest => dest.SessionType, opt => opt.MapFrom(src => src.SessionType.ToString()))
                .ForMember(dest => dest.SessionCategory, opt => opt.MapFrom(src => src.SessionCategory.ToString()))
                .ForMember(dest => dest.Mode, opt => opt.MapFrom(src => src.Mode.ToString()))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src =>
                    src.EndTime.HasValue ? (int?)(src.EndTime.Value - src.StartTime).TotalMinutes : null));


        }
    }
}

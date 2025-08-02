using AutoMapper;
using Motivision.Api.DTOs;
using Motivision.Core.Business.Entities;
using Motivision.Core.Identity.Entities;
using Motivision.API.Dtos;

namespace Motivision.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<RegisterDto, AppUser>();

            CreateMap<AppUser, UserDto>()
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            // Create
            CreateMap<CreateFocusSessionDto, FocusSession>();

            // End
            CreateMap<EndFocusSessionDto, FocusSession>();

            // Return (Details)
            CreateMap<FocusSession, DetailsFocusSessionDto>()
                .ForMember(dest => dest.SkillName, opt => opt.MapFrom(src => src.Skill != null ? src.Skill.Name : null));

            // Return (List)
            CreateMap<FocusSession, ListFocusSessionDto>();

            // Update
            CreateMap<UpdateFocusSessionDto, FocusSession>();

        }
    }
}
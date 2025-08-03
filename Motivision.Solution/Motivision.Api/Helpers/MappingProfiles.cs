using AutoMapper;
using Motivision.Api.DTOs;
using Motivision.Api.DTOs.GoalSteps;
using Motivision.Core.Business.Entities;
using Motivision.Core.Identity.Entities;
using Motivision.Api.DTOs.Goal;
using Motivision.Api.DTOs.Identity;
using Motivision.Api.DTOs.FocusSession;

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

            CreateMap<FocusSession, FocusSessionDto>();


            // Create
            CreateMap<CreateFocusSessionDto, FocusSession>();

            // End
            CreateMap<EndFocusSessionDto, FocusSession>();

            CreateMap<FocusSession, DetailsFocusSessionDto>();

            // Return (List)
            CreateMap<FocusSession, ListFocusSessionDto>();

            // Update
            CreateMap<UpdateFocusSessionDto, FocusSession>();

            CreateMap<Goal, GoalDto>()
            .ForMember(dest => dest.Progress,
                       opt => opt.MapFrom(src => CalculateProgress(src.Steps)))
            .ForMember(dest => dest.Steps,
                       opt => opt.MapFrom(src => src.Steps))
            .ReverseMap();


            CreateMap<GoalDto, Goal>().ReverseMap();


            CreateMap<GoalStep, GoalStepDto>().ReverseMap();

            CreateMap<CreateGoalStepDto, GoalStep>();
            CreateMap<UpdateGoalStepDto, GoalStep>();

            CreateMap<MarkGoalStepCompletedDto, GoalStep>();




        }
        private float CalculateProgress(ICollection<GoalStep> steps)
        {
            if (steps == null || steps.Count == 0) return 0;

            int completed = steps.Count(s => s.IsCompleted);
            return (float)completed / steps.Count * 100;
        }
    }
}
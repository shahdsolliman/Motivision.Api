using Motivision.Api.DTOs.GoalSteps;

namespace Motivision.Api.DTOs.Goal
{
    public class GoalDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }

        public float Progress { get; set; }  

        public List<GoalStepDto> Steps { get; set; } = new();
    }
}

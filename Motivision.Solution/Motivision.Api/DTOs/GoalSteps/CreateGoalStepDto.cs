namespace Motivision.Api.DTOs.GoalSteps
{
    public class CreateGoalStepDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int GoalId { get; set; }
    }

}

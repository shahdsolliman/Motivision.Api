namespace Motivision.Api.DTOs.GoalSteps
{
    public class GoalStepDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int GoalId { get; set; }
        public int? FocusSessionId { get; set; }
    }

}

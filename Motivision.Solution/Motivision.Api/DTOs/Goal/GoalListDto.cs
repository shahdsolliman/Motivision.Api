namespace Motivision.Api.DTOs.Goal
{
    public class GoalListDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public float Progress { get; set; }
    }

}

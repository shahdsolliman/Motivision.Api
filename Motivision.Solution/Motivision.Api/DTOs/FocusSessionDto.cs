namespace Motivision.Api.DTOs
{
    public class FocusSessionDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? Duration { get; set; }
        public string? Notes { get; set; }
        public string SessionType { get; set; }
        public string SessionCategory { get; set; }
        public string Mode { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsInterrupted { get; set; }
        public int? SkillId { get; set; }
    }
}

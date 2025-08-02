using Motivision.Core.Business.Enums;

namespace Motivision.Api.DTOs
{
    public class DetailsFocusSessionDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public string? Notes { get; set; }
        public FocusMode Mode { get; set; }
        public SessionType? SessionType { get; set; }
        public SessionCategory? SessionCategory { get; set; }
        public SessionStatus? SessionStatus { get; set; }

        public bool IsStarted { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public double? DurationMinutes =>
                (StartTime.HasValue && EndTime.HasValue)
                ? (EndTime.Value - StartTime.Value).TotalMinutes
                : null;

        public int? SkillId { get; set; }
        public string? SkillName { get; set; }

        public string UserId { get; set; } = default!;
    }
}

using Motivision.Core.Business.Enums;

namespace Motivision.Api.DTOs
{
    public class ListFocusSessionDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public SessionStatus? SessionStatus { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public double? DurationMinutes =>
        (StartTime.HasValue && EndTime.HasValue)
        ? (EndTime.Value - StartTime.Value).TotalMinutes
        : null;
    }
}

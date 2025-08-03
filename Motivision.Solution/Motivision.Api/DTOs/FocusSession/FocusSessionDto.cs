using Motivision.Core.Business.Enums;

namespace Motivision.Api.DTOs.FocusSession
{
    public class FocusSessionDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public SessionStatus SessionStatus { get; set; }
    }
}

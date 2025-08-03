using Motivision.Core.Business.Entities;
using Motivision.Core.Business.Enums;
using Motivision.Core.Identity.Entities;
using System.ComponentModel.DataAnnotations.Schema;

public class FocusSession : BaseEntity
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }

    public FocusMode Mode { get; set; }
    public SessionType? SessionType { get; set; }
    public SessionCategory? SessionCategory { get; set; }
    public SessionStatus? SessionStatus { get; set; }

    public bool IsStarted { get; set; } = false;
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }

    // Add duration explicitly for reporting
    [NotMapped]
    public TimeSpan? Duration => (StartTime.HasValue && EndTime.HasValue)
        ? EndTime.Value - StartTime.Value
        : null;

    // Future support (for streaks)
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Keep only the UserId as a reference to identity
    public string UserId { get; set; } = default!;

    // Optional reference to GoalStep
    public GoalStep? GoalStep { get; set; }
}

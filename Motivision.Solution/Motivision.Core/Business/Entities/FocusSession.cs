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
    public TimeSpan? Duration => (StartTime.HasValue && EndTime.HasValue)
        ? EndTime.Value - StartTime.Value
        : null;

    // Future support (for streaks)
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int? SkillId { get; set; }
    public Skill? Skill { get; set; }

    public string UserId { get; set; } = default!;
    public AppUser User { get; set; } = default!;
}

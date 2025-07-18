using Motivision.Core.Business.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Core.Business.Entities
{
    public class FocusSession : BaseEntity
    {
        public string UserId { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        [NotMapped]
        public int? Duration => EndTime.HasValue ? (int)(EndTime.Value - StartTime).TotalMinutes : null;

        public string? Notes { get; set; }
        public SessionType? SessionType { get; set; }
        public SessionCategory? SessionCategory { get; set; }
        public FocusMode Mode { get; set; } = FocusMode.Pomodoro;

        public bool IsCompleted { get; set; } = false;

        public bool IsInterrupted { get; set; } = false;


        public int? SkillId { get; set; }
        public Skill? Skill { get; set; }
    }
}

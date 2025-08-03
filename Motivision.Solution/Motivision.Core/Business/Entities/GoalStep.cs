using Motivision.Core.Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Core.Business.Entities
{
    public class GoalStep : BaseEntity
    {
        public string UserId { get; set; }

        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }

        public int GoalId { get; set; }
        public Goal Goal { get; set; }
        public int? FocusSessionId { get; set; }
        public FocusSession? FocusSession { get; set; }
    }
}

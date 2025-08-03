using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Core.Business.Entities
{
    public class Goal : BaseEntity
    {
        public string UserId { get; set; } = default!;

        public string Title { get; set; } = default!;
        public string? Description { get; set; } 

        [NotMapped]
        public int ProgressPercentage { get; set; }

        public bool IsCompleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<GoalStep> Steps { get; set; } = new List<GoalStep>();
    }
}

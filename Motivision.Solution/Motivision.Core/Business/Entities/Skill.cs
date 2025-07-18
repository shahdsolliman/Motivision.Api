using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Core.Business.Entities
{
    public class Skill : BaseEntity
    {
        public string UserId { get; set; }

        // Properties
        public string Name { get; set; }
        public int ProgressPercentage { get; set; }
        public string Category { get; set; }
        public DateTime LastUpdated { get; set; }

        // Navigation to sessions
        public ICollection<FocusSession> FocusSessions { get; set; } = new List<FocusSession>();
    }
}

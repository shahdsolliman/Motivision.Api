using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Core.Business.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}

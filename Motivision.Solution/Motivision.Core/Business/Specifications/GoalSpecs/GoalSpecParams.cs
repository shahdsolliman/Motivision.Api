using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Core.Business.Specifications.GoalSpecs
{
    public class GoalSpecParams
    {
        public string? UserId { get; set; }
        public bool? IsCompleted { get; set; }

        // Pagination
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        // Sorting
        public string? Sort { get; set; } // e.g., createdAsc, titleDesc
    }

}

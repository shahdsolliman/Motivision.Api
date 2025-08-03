using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Core.Business.Specifications.FocusSessionSpecs
{
    public class FocusSessionByUserAndDateRangeSpecification : BaseSpecifications<FocusSession>
    {
        public FocusSessionByUserAndDateRangeSpecification(string userId, DateTime from, DateTime to)
            : base(s => s.UserId == userId && s.CreatedAt >= from && s.CreatedAt <= to)
        {
            AddOrderByDesc(s => s.CreatedAt);
        }
    }

}

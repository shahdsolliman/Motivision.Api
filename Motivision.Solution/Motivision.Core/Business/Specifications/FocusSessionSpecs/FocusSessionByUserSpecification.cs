using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Core.Business.Specifications.FocusSessionSpecs
{
    public class FocusSessionByUserSpecification : BaseSpecifications<FocusSession>
    {
        public FocusSessionByUserSpecification(string userId)
            : base(s => s.UserId == userId)
        {
            AddOrderByDesc(s => s.CreatedAt);
        }
    }
}

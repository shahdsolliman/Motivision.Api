using Motivision.Core.Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Core.Business.Specifications.FocusSessionSpecs
{
    public class FocusSessionByUserAndStatusSpecification : BaseSpecifications<FocusSession>
    {
        public FocusSessionByUserAndStatusSpecification(string userId, SessionStatus status)
            : base(s => s.UserId == userId && s.SessionStatus == status)
        {
            AddOrderByDesc(s => s.CreatedAt);
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Core.Business.Specifications.FocusSessionSpecs
{
    public class FocusSessionCountSpecification : BaseSpecifications<FocusSession>
    {
        public FocusSessionCountSpecification(string userId)
            : base(s => s.UserId == userId)
        {
        }
    }

}

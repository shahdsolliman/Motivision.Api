using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Core.Business.Specifications.FocusSessionSpecs
{
    public class FocusSessionByGoalStepIdSpecification : BaseSpecifications<FocusSession>
    {
        public FocusSessionByGoalStepIdSpecification(int goalStepId)
            : base(s => s.GoalStep.Id == goalStepId)
        {
        }
    }

}

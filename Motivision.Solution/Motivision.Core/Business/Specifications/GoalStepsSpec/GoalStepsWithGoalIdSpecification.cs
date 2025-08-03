using Motivision.Core.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Core.Business.Specifications.GoalStepsSpec
{
    public class GoalStepsWithGoalIdSpecification : BaseSpecifications<GoalStep>
    {
        public GoalStepsWithGoalIdSpecification(int goalId, string userId)
            : base(s => s.GoalId == goalId && s.UserId == userId)
        {
            AddInclude(s => s.FocusSession); 
        }
    }
}

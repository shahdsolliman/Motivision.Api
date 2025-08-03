using Motivision.Core.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Core.Business.Specifications.GoalStepsSpec
{
    public class GoalStepWithIdAndUserSpec : BaseSpecifications<GoalStep>
    {
        public GoalStepWithIdAndUserSpec(int id, string userId)
            : base(s => s.Id == id && s.UserId == userId) { }
    }

}

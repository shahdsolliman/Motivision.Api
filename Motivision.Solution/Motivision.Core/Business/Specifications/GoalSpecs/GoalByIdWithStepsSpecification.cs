using Motivision.Core.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Core.Business.Specifications.GoalSpecs
{
    public class GoalByIdWithStepsSpecification : BaseSpecifications<Goal>
    {
        public GoalByIdWithStepsSpecification(int goalId, string userId)
            : base(g => g.Id == goalId && g.UserId == userId)
        {
            AddInclude(g => g.Steps);
        }
    }

}

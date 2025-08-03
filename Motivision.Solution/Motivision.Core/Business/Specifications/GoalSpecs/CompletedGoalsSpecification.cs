using Motivision.Core.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Core.Business.Specifications.GoalSpecs
{
    public class CompletedGoalsSpecification : BaseSpecifications<Goal>
    {
        public CompletedGoalsSpecification(string userId)
            : base(g => g.UserId == userId && g.Steps.All(s => s.IsCompleted))
        {
            AddInclude(g => g.Steps);
        }
    }

}

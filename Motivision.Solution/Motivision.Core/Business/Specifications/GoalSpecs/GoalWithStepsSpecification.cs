using Motivision.Core.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Core.Business.Specifications.GoalSpecs
{
    public class GoalWithStepsSpecification : BaseSpecifications<Goal>
    {
        public GoalWithStepsSpecification()
        {
            AddInclude(g => g.Steps);
        }
    }

}

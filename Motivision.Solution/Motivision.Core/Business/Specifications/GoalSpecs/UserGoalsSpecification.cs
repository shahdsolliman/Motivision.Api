using Motivision.Core.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Core.Business.Specifications.GoalSpecs
{
    public class UserGoalsSpecification : BaseSpecifications<Goal>
    {
        public UserGoalsSpecification(string userId)
            : base(g => g.UserId == userId)
        {
            AddOrderByDesc(g => g.CreatedAt);
        }
    }

}

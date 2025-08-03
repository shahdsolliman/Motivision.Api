using Motivision.Core.Business.Entities;
using Motivision.Core.Business.Specifications.GoalSpecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Core.Contracts.Services
{
    public interface IGoalService
    {
        Task<IReadOnlyList<Goal>> GetUserGoalsAsync(string userId);
        Task<Goal?> GetGoalWithStepsByIdAsync(int goalId, string userId);
        Task<IReadOnlyList<Goal>> GetPagedGoalsAsync(GoalSpecParams specParams);
        Task<int> CountGoalsAsync(GoalSpecParams specParams);

        Task<Goal> CreateGoalAsync(Goal goal);
        Task<bool> UpdateGoalAsync(Goal goal);
        Task<bool> DeleteGoalAsync(int goalId);
    }

}

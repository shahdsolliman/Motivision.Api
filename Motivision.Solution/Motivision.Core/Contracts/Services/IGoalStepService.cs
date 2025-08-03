using Motivision.Core.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Core.Contracts.Services
{
    public interface IGoalStepService
    {
        Task<GoalStep?> GetByIdAsync(int id, string userId);
        Task<IReadOnlyList<GoalStep>> GetAllByGoalIdAsync(int goalId, string userId);
        Task<GoalStep> CreateAsync(GoalStep step);
        Task<bool> UpdateAsync(GoalStep step, string userId);
        Task<bool> DeleteAsync(int id, string userId);
        Task<bool> MarkAsCompletedAsync(int id, string userId);
    }
}

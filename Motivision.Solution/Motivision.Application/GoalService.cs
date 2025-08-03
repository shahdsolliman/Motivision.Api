using Motivision.Core.Business.Entities;
using Motivision.Core.Business.Specifications.GoalSpecs;
using Motivision.Core.Contracts.Services;
using Motivision.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Application
{
    public class GoalService : IGoalService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GoalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<Goal>> GetUserGoalsAsync(string userId)
        {
            var spec = new UserGoalsSpecification(userId);
            return await _unitOfWork.Repository<Goal>().ListAsync(spec);
        }

        public async Task<Goal?> GetGoalWithStepsByIdAsync(int goalId, string userId)
        {
            var spec = new GoalByIdWithStepsSpecification(goalId, userId);
            return await _unitOfWork.Repository<Goal>().GetEntityWithSpecAsync(spec);
        }

        public async Task<bool> UpdateGoalAsync(Goal goal)
        {
            _unitOfWork.Repository<Goal>().Update(goal);
            return await _unitOfWork.CompleteAsync() > 0;
        }



        public async Task<IReadOnlyList<Goal>> GetPagedGoalsAsync(GoalSpecParams specParams)
        {
            var spec = new GoalWithParamsSpecification(specParams);
            return await _unitOfWork.Repository<Goal>().ListAsync(spec);
        }

        public async Task<int> CountGoalsAsync(GoalSpecParams specParams)
        {
            var spec = new GoalWithParamsSpecification(specParams);
            return await _unitOfWork.Repository<Goal>().CountAsync(spec);
        }

        public async Task<Goal> CreateGoalAsync(Goal goal)
        {
            await _unitOfWork.Repository<Goal>().AddAsync(goal);
            await _unitOfWork.CompleteAsync();
            return goal;
        }


        public async Task<bool> DeleteGoalAsync(int goalId)
        {
            var goal = await _unitOfWork.Repository<Goal>().GetAsync(goalId);
            if (goal == null) return false;

            _unitOfWork.Repository<Goal>().Delete(goal);
            return await _unitOfWork.CompleteAsync() > 0;
        }
    }

}

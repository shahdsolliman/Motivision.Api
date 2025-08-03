using Motivision.Core.Business.Entities;
using Motivision.Core.Contracts.Services;
using Motivision.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Motivision.Core.Business.Specifications.GoalStepsSpec;

namespace Motivision.Application
{
    public class GoalStepService : IGoalStepService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GoalStepService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GoalStep> CreateAsync(GoalStep step)
        {
            await _unitOfWork.Repository<GoalStep>().AddAsync(step);
            await _unitOfWork.CompleteAsync();
            return step;
        }

        public async Task<bool> DeleteAsync(int id, string userId)
        {
            var spec = new GoalStepWithIdAndUserSpec(id, userId);
            var step = await _unitOfWork.Repository<GoalStep>().GetEntityWithSpecAsync(spec);
            if (step is null) return false;

            _unitOfWork.Repository<GoalStep>().Delete(step);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IReadOnlyList<GoalStep>> GetAllByGoalIdAsync(int goalId, string userId)
        {
            var spec = new GoalStepsWithGoalIdSpecification(goalId, userId);
            return await _unitOfWork.Repository<GoalStep>().ListAsync(spec);
        }

        public async Task<GoalStep?> GetByIdAsync(int id, string userId)
        {
            var spec = new GoalStepWithIdAndUserSpec(id, userId);
            return await _unitOfWork.Repository<GoalStep>().GetEntityWithSpecAsync(spec);
        }

        public async Task<bool> UpdateAsync(GoalStep step, string userId)
        {
            var spec = new GoalStepWithIdAndUserSpec(step.Id, userId);
            var existing = await _unitOfWork.Repository<GoalStep>().GetEntityWithSpecAsync(spec);
            if (existing == null) return false;

            existing.Title = step.Title ?? existing.Title;
            existing.Description = step.Description ?? existing.Description;
            existing.FocusSessionId = step.FocusSessionId;

            _unitOfWork.Repository<GoalStep>().Update(existing);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> MarkAsCompletedAsync(int id, string userId)
        {
            var spec = new GoalStepWithIdAndUserSpec(id, userId);
            var step = await _unitOfWork.Repository<GoalStep>().GetEntityWithSpecAsync(spec);
            if (step == null) return false;

            step.IsCompleted = true;
            step.CompletedAt = DateTime.UtcNow;

            _unitOfWork.Repository<GoalStep>().Update(step);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }

}
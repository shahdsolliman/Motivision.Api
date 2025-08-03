using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Motivision.Api.DTOs.GoalSteps;
using Motivision.API.Errors;
using Motivision.Core.Business.Entities;
using Motivision.Core.Contracts.Services;
using System.Security.Claims;

namespace Motivision.Api.Controllers
{

    [Authorize]
    public class GoalStepsController : BaseApiController
    {
        private readonly IGoalStepService _goalStepService;
        private readonly IMapper _mapper;

        public GoalStepsController(IGoalStepService goalStepService, IMapper mapper)
        {
            _goalStepService = goalStepService;
            _mapper = mapper;
        }

        private string GetUserId() =>
            User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        // GET: api/GoalSteps/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GoalStepDto>> GetById(int id)
        {
            var step = await _goalStepService.GetByIdAsync(id, GetUserId());
            if (step == null)
                return NotFound(new ApiResponse(404, "Goal step not found"));

            return Ok(_mapper.Map<GoalStepDto>(step));
        }

        // GET: api/GoalSteps/goal/{goalId}
        [HttpGet("goal/{goalId}")]
        public async Task<ActionResult<IReadOnlyList<GoalStepDto>>> GetByGoalId(int goalId)
        {
            var steps = await _goalStepService.GetAllByGoalIdAsync(goalId, GetUserId());
            return Ok(_mapper.Map<IReadOnlyList<GoalStepDto>>(steps));
        }

        // POST: api/GoalSteps
        [HttpPost]
        public async Task<ActionResult<GoalStepDto>> Create([FromBody] CreateGoalStepDto dto)
        {
            var step = _mapper.Map<GoalStep>(dto);
            step.UserId = GetUserId();

            var created = await _goalStepService.CreateAsync(step);
            return Ok(_mapper.Map<GoalStepDto>(created));
        }

        // PUT: api/GoalSteps/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGoalStep(int id, UpdateGoalStepDto dto)
        {
            var userId = GetUserId();

            var existingStep = await _goalStepService.GetByIdAsync(id, userId);
            if (existingStep is null)
                return NotFound(new ApiResponse(404, "Step not found or unauthorized"));

            dto.Id = id; 
            _mapper.Map(dto, existingStep);

            var result = await _goalStepService.UpdateAsync(existingStep, userId);
            if (!result) return BadRequest(new ApiResponse(400, "Update failed"));

            return Ok(_mapper.Map<GoalStepDto>(existingStep));
        }



        // DELETE: api/GoalSteps/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _goalStepService.DeleteAsync(id, GetUserId());
            if (!success)
                return NotFound(new ApiResponse(404, "Goal step not found or not yours"));

            return Ok(new ApiResponse(200, "Deleted successfully"));
        }

        // PATCH: api/GoalSteps/{id}/complete
        [HttpPatch("{id}/complete")]
        public async Task<ActionResult> MarkAsCompleted(int id)
        {
            var success = await _goalStepService.MarkAsCompletedAsync(id, GetUserId());
            if (!success)
                return NotFound(new ApiResponse(404, "Goal step not found or not yours"));

            return Ok(new ApiResponse(200, "Marked as completed"));
        }
    }
}

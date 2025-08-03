using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Motivision.Api.DTOs.Goal;
using Motivision.API.Errors;
using Motivision.Core.Business.Entities;
using Motivision.Core.Contracts.Services;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Motivision.Api.Controllers
{
    [Authorize]
    public class GoalController : BaseApiController
    {
        private readonly IGoalService _goalService;
        private readonly IMapper _mapper;

        public GoalController(IGoalService goalService, IMapper mapper)
        {
            _goalService = goalService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<GoalDto>>> GetGoals()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new ApiResponse(401));

            var goals = await _goalService.GetUserGoalsAsync(userId);
            return Ok(_mapper.Map<IReadOnlyList<GoalDto>>(goals));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GoalDto>> GetGoalById(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var goal = await _goalService.GetGoalWithStepsByIdAsync(id, userId);
            if (goal == null)
                return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<GoalDto>(goal));
        }

        [HttpPost]
        public async Task<ActionResult<GoalDto>> CreateGoal([FromBody] GoalDto goalDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new ApiValidationErrorResponse { Errors = errors });
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new ApiResponse(401));

            var goal = _mapper.Map<Goal>(goalDto);
            goal.UserId = userId;

            var created = await _goalService.CreateGoalAsync(goal);
            return CreatedAtAction(nameof(GetGoalById), new { id = created.Id }, _mapper.Map<GoalDto>(created));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGoal(int id, [FromBody] GoalDto goalDto)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new ApiValidationErrorResponse { Errors = errors });
            }

            if (id != goalDto.Id)
                return BadRequest(new ApiResponse(400, "ID mismatch between route and body"));

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new ApiResponse(401));

            var existing = await _goalService.GetGoalWithStepsByIdAsync(id, userId);
            if (existing == null)
                return NotFound(new ApiResponse(404, "Goal not found for this user"));

            var updated = _mapper.Map(goalDto, existing);
            var success = await _goalService.UpdateGoalAsync(existing);

            if (!success)
                return BadRequest(new ApiResponse(400, "Failed to update goal"));

            var updatedDto = _mapper.Map<GoalDto>(updated);
            return Ok(updatedDto);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoal(int id)
        {
            var success = await _goalService.DeleteGoalAsync(id);
            if (!success)
                return NotFound(new ApiResponse(404, "Goal not found"));

            return NoContent();
        }
    }
}

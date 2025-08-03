using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Motivision.Api.Controllers;
using Motivision.Api.DTOs;
using Motivision.Api.DTOs.FocusSession;
using Motivision.API.Errors;
using Motivision.Application.Services;
using Motivision.Core.Business.Enums;
using Motivision.Core.Contracts.Services;
using Motivision.Core.Contracts.Services.Contracts;
using System.Security.Claims;

namespace Motivision.API.Controllers
{
    [Authorize]
    public class FocusSessionsController : BaseApiController
    {
        private readonly IFocusSessionService _focusSessionService;
        private readonly IMapper _mapper;

        public FocusSessionsController(IFocusSessionService sessionService,
            IMapper mapper)
        {
            _focusSessionService = sessionService;
            _mapper = mapper;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new UnauthorizedAccessException();
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<FocusSessionDto>>> GetAll()
        {
            var userId = GetUserId();
            var sessions = await _focusSessionService.GetAllSessionsAsync(userId!);
            return Ok(_mapper.Map<IReadOnlyList<FocusSessionDto>>(sessions));
        }

        [HttpGet("paginated")]
        public async Task<ActionResult<IReadOnlyList<FocusSessionDto>>> GetPaginated([FromQuery] int skip = 0, [FromQuery] int take = 10, [FromQuery] string? sort = null)
        {
            var userId = GetUserId();
            var sessions = await _focusSessionService.GetSessionsWithPaginationAsync(userId!, skip, take, sort);
            return Ok(_mapper.Map<IReadOnlyList<FocusSessionDto>>(sessions));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FocusSessionDto>> GetById(int id)
        {
            var userId = GetUserId();
            var session = await _focusSessionService.GetByIdAsync(id, userId!);

            if (session == null)
                return NotFound(new ApiResponse(404, "Focus session not found."));

            return Ok(_mapper.Map<FocusSessionDto>(session));
        }

        [HttpPost]
        public async Task<ActionResult<FocusSessionDto>> Create([FromBody] CreateFocusSessionDto dto)
        {
            var userId = GetUserId();

            var session = _mapper.Map<FocusSession>(dto);
            session.UserId = userId!;
            session.CreatedAt = DateTime.UtcNow;
            session.SessionStatus = Core.Business.Enums.SessionStatus.Pending;

            var created = await _focusSessionService.CreateSessionAsync(session);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.Map<FocusSessionDto>(created));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateFocusSessionDto dto)
        {
            var userId = GetUserId();
            var session = _mapper.Map<FocusSession>(dto);
            session.Id = id;

            var success = await _focusSessionService.UpdateSessionAsync(session, userId!);
            if (!success)
                return NotFound(new ApiResponse(404, "Focus session not found or unauthorized."));

            var updatedSession = await _focusSessionService.GetByIdAsync(id, userId!);
            return Ok(_mapper.Map<FocusSessionDto>(updatedSession));

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = GetUserId();
            var success = await _focusSessionService.DeleteSessionAsync(id, userId!);
            if (!success)
                return NotFound(new ApiResponse(404, "Focus session not found or unauthorized."));

            return Ok(new ApiResponse(200, "Focus session deleted successfully."));
        }

        [HttpPost("start")]
        public async Task<ActionResult> StartSession([FromBody] StartFocusSessionDto dto)
        {
            var userId = GetUserId();
            var success = await _focusSessionService.StartSessionAsync(dto.Id, userId!);

            if (!success)
                return BadRequest(new ApiResponse(400, "Cannot start session."));

            return Ok(new ApiResponse(200, "Focus session started successfully."));
        }

        [HttpPost("end")]
        public async Task<ActionResult> EndSession([FromBody] EndFocusSessionDto dto)
        {
            var userId = GetUserId();
            var success = await _focusSessionService.EndSessionAsync(dto.Id, userId!);

            if (!success)
                return BadRequest(new ApiResponse(400, "Cannot end session."));

            return Ok(new ApiResponse(200, "Focus session ended successfully."));
        }


        [HttpGet("range")]
        public async Task<ActionResult<IReadOnlyList<FocusSessionDto>>> GetByDateRange([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var userId = GetUserId();
            var sessions = await _focusSessionService.GetSessionsByDateAsync(userId!, from, to);
            return Ok(_mapper.Map<IReadOnlyList<FocusSessionDto>>(sessions));
        }

        [HttpGet("streak")]
        public async Task<ActionResult<int>> GetStreak()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new ApiResponse(401, "User is not authorized."));
            }

            var streak = await _focusSessionService.CalculateUserStreakAsync(userId);
            return Ok(streak);
        }

    }
}

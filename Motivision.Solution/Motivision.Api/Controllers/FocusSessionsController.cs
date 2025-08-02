using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Motivision.Api.Controllers;
using Motivision.Api.DTOs;
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

        [HttpPost]
        public async Task<ActionResult<DetailsFocusSessionDto>> CreateSession(CreateFocusSessionDto dto)
        {
            var userId = GetUserId();
            var session = _mapper.Map<FocusSession>(dto);
            session.UserId = userId;

            var created = await _focusSessionService.CreateSessionAsync(session);
            var result = _mapper.Map<DetailsFocusSessionDto>(created);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPost("start")]
        public async Task<ActionResult> StartSession(StartFocusSessionDto dto)
        {
            var userId = GetUserId();
            var success = await _focusSessionService.StartSessionAsync(dto.Id, userId);

            if (!success)
                return BadRequest(new ApiResponse(400, "Session could not be started."));

            return Ok(new ApiResponse(200, "Session started successfully."));
        }

        [HttpPost("end")]
        public async Task<ActionResult> EndSession(EndFocusSessionDto dto)
        {
            var userId = GetUserId();
            var success = await _focusSessionService.EndSessionAsync(dto.Id, userId);

            if (!success)
                return BadRequest(new ApiResponse(400, "Session could not be ended."));

            return Ok(new ApiResponse(200, "Session ended successfully."));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSession(UpdateFocusSessionDto dto)
        {
            var userId = GetUserId();
            var session = _mapper.Map<UpdateFocusSessionDto, FocusSession>(dto);
            var success = await _focusSessionService.UpdateSessionAsync(session, userId);

            if (!success)
                return NotFound(new ApiResponse(404, "Session not found or unauthorized"));

            return Ok(new ApiResponse(200, "Session updated successfully."));
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSession(int id)
        {
            var userId = GetUserId();
            var success = await _focusSessionService.DeleteSessionAsync(id, userId);

            if (!success)
                return NotFound(new ApiResponse(404, "Session not found or unauthorized"));

            return Ok(new ApiResponse(200, "Session deleted successfully."));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<DetailsFocusSessionDto>> GetById(int id)
        {
            var userId = GetUserId();
            var session = await _focusSessionService.GetByIdAsync(id, userId);

            if (session == null)
                return NotFound(new ApiResponse(404, "Session not found."));

            return Ok(_mapper.Map<DetailsFocusSessionDto>(session));
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ListFocusSessionDto>>> GetAll()
        {
            var userId = GetUserId();
            var sessions = await _focusSessionService.GetAllSessionsAsync(userId);

            return Ok(_mapper.Map<IReadOnlyList<ListFocusSessionDto>>(sessions));
        }
    }
}

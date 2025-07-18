using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Motivision.Api.Controllers;
using Motivision.Api.DTOs;
using Motivision.API.Errors;
using Motivision.Application.Features.FocusSessions.Commands;
using Motivision.Application.Features.FocusSessions.Queries;
using Motivision.Application.Interfaces;
using Motivision.Application.Services;
using Motivision.Core.Contracts.Services.Contracts;
using System.Security.Claims;

namespace Motivision.API.Controllers
{
    [Authorize]
    public class FocusSessionsController : BaseApiController
    {
        private readonly IFocusSessionService _sessionService;
        private readonly IMapper _mapper;

        public FocusSessionsController(IFocusSessionService sessionService,
            IMapper mapper)
        {
            _sessionService = sessionService;
            _mapper = mapper;
        }

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        [HttpGet]
        public async Task<IActionResult> GetAllUserSessions()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var entities = await _sessionService.GetUserSessionsAsync(userId);
            var dtos = _mapper.Map<List<FocusSessionDto>>(entities);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSessionById(int id)
        {
            var userId = GetUserId();
            var entity = await _sessionService.GetByIdAsync(id, userId);
            if (entity == null) return NotFound();

            var dto = _mapper.Map<FocusSessionDto>(entity);
            return Ok(dto);
        }

        [Authorize]
        [HttpPost("start")]
        public async Task<IActionResult> StartSession([FromBody] CreateFocusSessionCommand command)
        {
            command.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var result = await _sessionService.StartSessionAsync(command);
            return Ok(result);
        }



        [HttpPost("end")]
        public async Task<IActionResult> EndSession([FromBody] EndFocusSessionCommand command)
        {
            var result = await _sessionService.EndSessionAsync(command);
            return result ? Ok(new { Message = "Session ended." }) : NotFound();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateSession([FromBody] UpdateFocusSessionCommand command)
        {
            var result = await _sessionService.UpdateSessionAsync(command);
            return result ? Ok(new { Message = "Session updated." }) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(int id)
        {
            var result = await _sessionService.DeleteSessionAsync(id);
            return result ? Ok(new { Message = "Session deleted." }) : NotFound();
        }


    }
}

using Motivision.Application.Interfaces;
using Motivision.Core.Business.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.IdentityModel.Tokens;
using Motivision.Infrastructure.Persistence;
using MediatR;
using Motivision.Application.Features.FocusSessions.Commands;
using Motivision.Application.Features.FocusSessions.Queries;

namespace Motivision.Application.Services
{
    public class FocusSessionService : IFocusSessionService
    {
        private readonly IMediator _mediator;

        public FocusSessionService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<int> StartSessionAsync(CreateFocusSessionCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<bool> EndSessionAsync(EndFocusSessionCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<bool> UpdateSessionAsync(UpdateFocusSessionCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<bool> DeleteSessionAsync(int sessionId)
        {
            return await _mediator.Send(new DeleteFocusSessionCommand { SessionId = sessionId });
        }

        public async Task<List<FocusSession>> GetUserSessionsAsync(string userId)
        {
            return await _mediator.Send(new GetUserSessionsQuery { UserId = userId });
        }

        public async Task<FocusSession?> GetByIdAsync(int id, string userId)
        {
            return await _mediator.Send(new GetFocusSessionByIdQuery { Id = id, UserId = userId });
        }


    }
}

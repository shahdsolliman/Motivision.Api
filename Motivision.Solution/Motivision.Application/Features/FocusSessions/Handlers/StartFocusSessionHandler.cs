using MediatR;
using Motivision.Application.Features.FocusSessions.Commands;
using Motivision.Core.Business.Entities;
using Motivision.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Application.Features.FocusSessions.Handlers
{
    public class StartFocusSessionHandler : IRequestHandler<StartFocusSessionCommand, int>
    {
        private readonly AppBusinessDbContext _context;

        public StartFocusSessionHandler(AppBusinessDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(StartFocusSessionCommand request, CancellationToken cancellationToken)
        {
            var session = new FocusSession
            {
                StartTime = request.StartTime,
                Notes = request.Notes,
                SessionType = request.SessionType,
                SessionCategory = request.SessionCategory,
                Mode = request.Mode,
                SkillId = request.SkillId,
                UserId = request.UserId
            };

            _context.FocusSessions.Add(session);
            await _context.SaveChangesAsync(cancellationToken);

            return session.Id;
        }
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using Motivision.Application.Features.FocusSessions.Commands;
using Motivision.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Motivision.Application.Features.FocusSessions.Handlers
{
    public class UpdateFocusSessionHandler : IRequestHandler<UpdateFocusSessionCommand, bool>
    {
        private readonly AppBusinessDbContext _context;

        public UpdateFocusSessionHandler(AppBusinessDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateFocusSessionCommand request, CancellationToken cancellationToken)
        {
            var session = await _context.FocusSessions.FindAsync(new object[] { request.Id }, cancellationToken);
            if (session == null) return false;

            if (request.Notes != null)
                session.Notes = request.Notes;

            if (request.IsCompleted.HasValue)
                session.IsCompleted = request.IsCompleted.Value;

            if (request.IsInterrupted.HasValue)
                session.IsInterrupted = request.IsInterrupted.Value;

            if (request.SessionType.HasValue)
                session.SessionType = request.SessionType.Value;

            if (request.SessionCategory.HasValue)
                session.SessionCategory = request.SessionCategory.Value;

            if (request.Mode.HasValue)
                session.Mode = request.Mode.Value;

            if (request.SkillId.HasValue)
                session.SkillId = request.SkillId;

            _context.FocusSessions.Update(session);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}

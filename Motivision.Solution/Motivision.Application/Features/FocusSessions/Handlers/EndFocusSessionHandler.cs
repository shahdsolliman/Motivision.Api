using MediatR;
using Motivision.Application.Features.FocusSessions.Commands;
using Motivision.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Application.Features.FocusSessions.Handlers
{
    public class EndFocusSessionHandler : IRequestHandler<EndFocusSessionCommand, bool>
    {
        private readonly AppBusinessDbContext _context;

        public EndFocusSessionHandler(AppBusinessDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(EndFocusSessionCommand request, CancellationToken cancellationToken)
        {
            var session = await _context.FocusSessions.FindAsync(request.SessionId);
            if (session == null)
                return false;

            session.EndTime = request.EndTime;
            session.Notes += "\n" + request.FinalNotes;

            _context.FocusSessions.Update(session);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}

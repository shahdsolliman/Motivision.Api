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
    public class DeleteFocusSessionHandler : IRequestHandler<DeleteFocusSessionCommand, bool>
    {
        private readonly AppBusinessDbContext _context;

        public DeleteFocusSessionHandler(AppBusinessDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteFocusSessionCommand request, CancellationToken cancellationToken)
        {
            var session = await _context.FocusSessions.FindAsync(request.SessionId);
            if (session == null) return false;

            _context.FocusSessions.Remove(session);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}

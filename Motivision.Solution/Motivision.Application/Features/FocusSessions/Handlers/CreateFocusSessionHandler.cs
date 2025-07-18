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
    public class CreateFocusSessionHandler : IRequestHandler<CreateFocusSessionCommand, int>
    {
        private readonly AppBusinessDbContext _context;

        public CreateFocusSessionHandler(AppBusinessDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateFocusSessionCommand request, CancellationToken cancellationToken)
        {
            var session = new FocusSession
            {
                StartTime = request.StartTime,
                Notes = request.Notes,
                Mode = request.Mode,
                SessionType = request.SessionType,
                SessionCategory = request.SessionCategory,
                SkillId = request.SkillId,
                UserId = request.UserId
            };

            await _context.FocusSessions.AddAsync(session, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return session.Id;
        }
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using Motivision.Application.Features.FocusSessions.Queries;
using Motivision.Core.Business.Entities;
using Motivision.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Application.Features.FocusSessions.Handlers
{
    public class GetUserSessionsHandler : IRequestHandler<GetUserSessionsQuery, List<FocusSession>>
    {
        private readonly AppBusinessDbContext _context;

        public GetUserSessionsHandler(AppBusinessDbContext context)
        {
            _context = context;
        }

        public async Task<List<FocusSession>> Handle(GetUserSessionsQuery request, CancellationToken cancellationToken)
        {
            return await _context.FocusSessions
                .Where(s => s.UserId == request.UserId)
                .OrderByDescending(s => s.StartTime)
                .ToListAsync(cancellationToken);
        }
    }
}
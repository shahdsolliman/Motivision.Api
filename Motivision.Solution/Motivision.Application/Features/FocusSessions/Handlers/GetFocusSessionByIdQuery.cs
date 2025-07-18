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
    public class GetFocusSessionByIdHandler : IRequestHandler<GetFocusSessionByIdQuery, FocusSession?>
    {
        private readonly AppBusinessDbContext _context;

        public GetFocusSessionByIdHandler(AppBusinessDbContext context)
        {
            _context = context;
        }

        public async Task<FocusSession?> Handle(GetFocusSessionByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.FocusSessions
                .FirstOrDefaultAsync(s => s.Id == request.Id && s.UserId == request.UserId, cancellationToken);
        }
    }
}

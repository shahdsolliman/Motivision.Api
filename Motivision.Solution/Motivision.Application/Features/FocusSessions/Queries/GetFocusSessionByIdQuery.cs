using MediatR;
using Motivision.Core.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Application.Features.FocusSessions.Queries
{
    public class GetFocusSessionByIdQuery : IRequest<FocusSession?>
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
    }

}

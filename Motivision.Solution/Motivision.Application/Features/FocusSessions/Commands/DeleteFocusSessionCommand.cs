using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Application.Features.FocusSessions.Commands
{
    public class DeleteFocusSessionCommand : IRequest<bool>
    {
        public int SessionId { get; set; }
    }
}

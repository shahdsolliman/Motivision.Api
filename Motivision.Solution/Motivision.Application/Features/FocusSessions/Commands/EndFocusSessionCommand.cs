using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Application.Features.FocusSessions.Commands
{
    public class EndFocusSessionCommand : IRequest<bool>
    {
        public int SessionId { get; set; }
        public DateTime EndTime { get; set; }
        public string? FinalNotes { get; set; }
    }
}

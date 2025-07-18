using MediatR;
using Motivision.Core.Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Motivision.Application.Features.FocusSessions.Commands
{
    public class CreateFocusSessionCommand : IRequest<int>
    {
        public DateTime StartTime { get; set; }
        public string? Notes { get; set; }
        public FocusMode Mode { get; set; }
        public SessionType? SessionType { get; set; }
        public SessionCategory? SessionCategory { get; set; }
        public int? SkillId { get; set; }

        public string UserId { get; set; } = default!;

    }
}

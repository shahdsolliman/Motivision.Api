using MediatR;
using Motivision.Core.Business.Enums;

namespace Motivision.Application.Features.FocusSessions.Commands
{
    public class UpdateFocusSessionCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string? Notes { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsInterrupted { get; set; }
        public SessionType? SessionType { get; set; }
        public SessionCategory? SessionCategory { get; set; }
        public FocusMode? Mode { get; set; }
        public int? SkillId { get; set; }
    }
}

using Motivision.Core.Business.Enums;

namespace Motivision.Api.DTOs
{
    public class CreateFocusSessionDto
    {
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public string? Notes { get; set; }
        public FocusMode Mode { get; set; }
        public SessionType? SessionType { get; set; }
        public SessionCategory? SessionCategory { get; set; }
    }
}

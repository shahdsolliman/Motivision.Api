﻿namespace Motivision.Api.DTOs
{
    public class UpdateFocusSessionDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Notes { get; set; }
        public int? SkillId { get; set; }
    }
}

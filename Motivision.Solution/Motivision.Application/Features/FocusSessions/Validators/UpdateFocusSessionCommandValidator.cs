using FluentValidation;
using Motivision.Application.Features.FocusSessions.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Application.Features.FocusSessions.Validators
{
    public class UpdateFocusSessionCommandValidator : AbstractValidator<UpdateFocusSessionCommand>
    {
        public UpdateFocusSessionCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Session ID must be greater than 0.");

            When(x => x.Notes is not null, () =>
            {
                RuleFor(x => x.Notes)
                    .MaximumLength(500).WithMessage("Notes must not exceed 500 characters.");
            });

            When(x => x.SkillId.HasValue, () =>
            {
                RuleFor(x => x.SkillId.Value)
                    .GreaterThan(0).WithMessage("Skill ID must be greater than 0.");
            });
        }
    }
}

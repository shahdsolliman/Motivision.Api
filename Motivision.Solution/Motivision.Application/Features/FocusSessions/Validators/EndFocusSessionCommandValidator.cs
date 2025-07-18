using FluentValidation;
using Motivision.Application.Features.FocusSessions.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Application.Features.FocusSessions.Validators
{
    public class EndFocusSessionCommandValidator : AbstractValidator<EndFocusSessionCommand>
    {
        public EndFocusSessionCommandValidator()
        {
            RuleFor(x => x.SessionId)
                .GreaterThan(0).WithMessage("Session ID must be greater than 0.");

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("End Time is required.");
        }
    }
}

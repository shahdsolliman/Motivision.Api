using FluentValidation;
using Motivision.Application.Features.FocusSessions.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Application.Features.FocusSessions.Validators
{
    public class CreateFocusSessionValidator : AbstractValidator<CreateFocusSessionCommand>
    {
        public CreateFocusSessionValidator()
        {
            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("Start time is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Start time can't be in the future.");

            RuleFor(x => x.Mode)
                .IsInEnum().WithMessage("Invalid focus mode.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.");
        }
    }
}

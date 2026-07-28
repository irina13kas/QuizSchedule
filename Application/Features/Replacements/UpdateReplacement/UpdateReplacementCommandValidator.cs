using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Replacements.UpdateReplacement
{
    public class UpdateReplacementCommandValidator : AbstractValidator<UpdateReplacementCommand>
    {
        public UpdateReplacementCommandValidator() {
            RuleFor(x => x.ReplacementId)
                .NotEmpty().WithMessage("Id замены обязателен");

            RuleFor(x => x.IsFullShift)
                .NotEmpty().WithMessage("Поле смены должно быть запомлено");
        }
    }
}

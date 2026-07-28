using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Replacements.TakeReplacement
{
    public class TakeReplacementCommandValidator : AbstractValidator<TakeReplacementCommand>
    {
        public TakeReplacementCommandValidator() {
            RuleFor(x => x.ReplacementId)
                .NotEmpty().WithMessage("Id Замены обязателен");
        }
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Replacements.DeleteReplacement
{
    public class DeleteReplacementCommandValidator : AbstractValidator<DeleteReplacementCommand>
    {
        public DeleteReplacementCommandValidator() 
        {
            RuleFor(x => x.ReplacementId)
                .NotEmpty().WithMessage("Id Замены обязателен");
        }
    }
}

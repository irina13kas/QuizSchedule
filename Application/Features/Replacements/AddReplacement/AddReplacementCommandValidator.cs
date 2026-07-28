using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Replacements.AddReplacement
{
    public class AddReplacementCommandValidator : AbstractValidator<AddReplacementCommand>
    {
        public AddReplacementCommandValidator() {
            RuleFor(x => x.QuizmanId)
                .NotEmpty().WithMessage("Id Квизмена обязателен");

            RuleFor(x => x.IsFullShift)
                .NotEmpty().WithMessage("Смена не может быть пустой");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Дата обязательна")
                .GreaterThanOrEqualTo(DateTime.Today);
        }
    }
}

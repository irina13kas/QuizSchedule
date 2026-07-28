using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Profiles.DeleteQuizemanProfile
{
    public class DeleteQuizmanProfileCommandValidator : AbstractValidator<DeleteQuizmanProfileCommand>
    {
        public DeleteQuizmanProfileCommandValidator() {
            RuleFor(x => x.QuizmanId)
                .NotEmpty().WithMessage("Id Квизмена обязателен");
        }
    }
}

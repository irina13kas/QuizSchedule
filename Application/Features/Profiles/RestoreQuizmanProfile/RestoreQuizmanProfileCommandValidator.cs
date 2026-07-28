using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Profiles.RestoreQuizemanProfile
{
    public class RestoreQuizmanProfileCommandValidator : AbstractValidator<RestoreQuizmanProfileCommand>
    {
        public RestoreQuizmanProfileCommandValidator() 
        {
            RuleFor(x => x.QuizmanId)
                .NotEmpty().WithMessage("Id Квизмена обязателен");
        }
    }
}

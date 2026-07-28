using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Profiles.GetMyProfile
{
    public class GetQuizmanProfileQueryValidator : AbstractValidator<GetQuizmanProfileQuery>
    {
        public GetQuizmanProfileQueryValidator()
        {
            RuleFor(x => x.QuizmanId)
                .NotEmpty().WithMessage("Id Квизмена обязателен");
        }
    }
}

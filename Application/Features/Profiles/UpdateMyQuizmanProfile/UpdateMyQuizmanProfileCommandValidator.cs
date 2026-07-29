using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Profiles.UpdateMyProfile
{
    public class UpdateMyQuizmanProfileCommandValidator : AbstractValidator<UpdateMyQuizmanProfileCommand>
    {
        public UpdateMyQuizmanProfileCommandValidator()
        {
            RuleFor(x => x.BirthDay)
                .LessThan(x => DateTime.Today)
                .When(x => x.BirthDay.HasValue);
        }
    }
}

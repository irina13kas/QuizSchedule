using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Profiles.UpdateMyAdminProfile
{
    public class UpdateMyAdminProfileCommandValidator : AbstractValidator<UpdateMyAdminProfileCommand>
    {
        public UpdateMyAdminProfileCommandValidator()
        {
            RuleFor(x => x.AdminId)
                .NotEmpty().WithMessage("Id Квизмена обязателен");

            RuleFor(x => x.BirthDay)
                .LessThan(x => DateTime.Today)
                .When(x => x.BirthDay.HasValue);
        }
    }
}

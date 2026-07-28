using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Profiles.GetAdminProfile
{
    public class GetAdminProfileQueryValidator : AbstractValidator<GetAdminProfileQuery>
    {
        public GetAdminProfileQueryValidator() 
        {
            RuleFor(x => x.AdminId)
                .NotEmpty().WithMessage("Id Администратора обязателен");
        }
    }
}

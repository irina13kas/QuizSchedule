using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Djs.GetDj
{
    public class GetDjQueryValidator : AbstractValidator<GetDjQuery>
    {
        public GetDjQueryValidator() {
            RuleFor(x => x.DjId)
                .NotEmpty().WithMessage("Id Dj обязателен");
        }
    }
}

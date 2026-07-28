using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Bars.GetBar
{
    public class GetBarQueryValidator : AbstractValidator<GetBarQuery>
    {
        public GetBarQueryValidator() {
            RuleFor(x => x.BarId)
                .NotEmpty().WithMessage("Id бара обязателен");
        }
    }
}

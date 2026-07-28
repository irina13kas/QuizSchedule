using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Bars.AddBar
{
    public class AddBarCommandValidator : AbstractValidator<AddBarCommand>
    {
        public AddBarCommandValidator() {
            RuleFor(x => x.BarName)
                .NotEmpty().WithMessage("Название бара обязательно");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Название бара обязательно");
        }
    }
}

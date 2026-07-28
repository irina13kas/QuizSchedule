using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Bars.UpdateBar
{
    public class UpdateBarCommandValidator : AbstractValidator<UpdateBarCommand>
    {
        public UpdateBarCommandValidator() {
            RuleFor(x => x.BarId)
                .NotEmpty().WithMessage("Id бара обязателен");
        }
    }
}

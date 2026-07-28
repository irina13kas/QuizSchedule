using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Bars.DeleteBar
{
    public class DeleteBarCommandValidator : AbstractValidator<DeleteBarCommand>
    {
        public DeleteBarCommandValidator() {
            RuleFor(x => x.BarId)
                .NotEmpty().WithMessage("Id бара обязателен");
        }
    }
}

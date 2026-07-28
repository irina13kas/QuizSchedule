using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Djs.AddDj
{
    public class AddDjCommandValidator : AbstractValidator<AddDjCommand>
    {
        public AddDjCommandValidator() {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя dj обязательно");
        }
    }
}

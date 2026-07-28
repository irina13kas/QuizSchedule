using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Masters.AddMaster
{
    public class AddMasterCommandValidator: AbstractValidator<AddMasterCommand>
    {
        public AddMasterCommandValidator() {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя Ведущего обязательно");
        }
    }
}

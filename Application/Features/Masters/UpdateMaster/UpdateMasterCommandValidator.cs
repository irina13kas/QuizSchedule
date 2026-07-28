using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Masters.UpdateMaster
{
    public class UpdateMasterCommandValidator : AbstractValidator<UpdateMasterCommand>
    {
        public UpdateMasterCommandValidator() {
            RuleFor(x => x.MasterId)
                .NotEmpty().WithMessage("Id Ведущего обязателен");
        }
    }
}

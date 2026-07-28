using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Masters.DeleteMaster
{
    public class DeleteMasterCommandValidator : AbstractValidator<DeleteMasterCommand>
    {
        public DeleteMasterCommandValidator() {
            RuleFor(x => x.MasterId)
                .NotEmpty().WithMessage("Id Ведущего обязателен");
        }
    }
}

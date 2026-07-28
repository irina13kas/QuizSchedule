using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FinesHistory.DeleteFine
{
    public class DeleteFineCommandValidator : AbstractValidator<DeleteFineCommand>
    {
        public DeleteFineCommandValidator() {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id не может быть пустым");
        }
    }
}

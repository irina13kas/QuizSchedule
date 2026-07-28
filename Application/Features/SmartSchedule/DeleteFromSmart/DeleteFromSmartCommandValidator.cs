using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SmartSchedule.DeleteFromSmart
{
    public class DeleteFromSmartCommandValidator : AbstractValidator<DeleteFromSmartCommand>
    {
        public DeleteFromSmartCommandValidator() {
            RuleFor(x => x.SmartId)
                .NotEmpty().WithMessage("Id дня Smart обязателен");
        }
    }
}

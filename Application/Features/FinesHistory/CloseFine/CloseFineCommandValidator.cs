using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FinesHistory.CloseFine
{
    public class CloseFineCommandValidator : AbstractValidator<CloseFineCommand>
    {
        public CloseFineCommandValidator()
        {
            RuleFor(x => x.FineId)
                .NotEmpty().WithMessage("Id штрафа обязателен");
        }
    }
}

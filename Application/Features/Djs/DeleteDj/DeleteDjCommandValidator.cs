using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Djs.DeleteDj
{
    public class DeleteDjCommandValidator : AbstractValidator<DeleteDjCommand>
    {
        public DeleteDjCommandValidator() {
            RuleFor(x => x.DjId)
                .NotEmpty().WithMessage("Id dj обязателен");
        }
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Djs.UpdateDj
{
    public class UpdateDjCommandValidator : AbstractValidator<UpdateDjCommand>
    {
        public UpdateDjCommandValidator() {
            RuleFor(x => x.DjId)
                .NotEmpty().WithMessage("Id Dj обязателен");
        }
    }
}

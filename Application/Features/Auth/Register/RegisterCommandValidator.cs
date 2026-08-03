using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator() {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя обязательно");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Роль обязательна");

            RuleFor(x => x.VKUrl)
                .NotEmpty().WithMessage("ВК обязателен");
        }
    }
}

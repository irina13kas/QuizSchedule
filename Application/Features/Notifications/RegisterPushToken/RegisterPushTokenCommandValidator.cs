using Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notifications.RegisterPushToken
{
    public class RegisterPushTokenCommandValidator : AbstractValidator<RegisterPushTokenCommand>
    {
        public RegisterPushTokenCommandValidator() {
            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Токен обязателен");

            RuleFor(x => x.Platform)
                .NotEmpty().WithMessage("Платформа обязательна")
                .IsEnumName(typeof(PlatformType), caseSensitive: false);
        }
    }
}

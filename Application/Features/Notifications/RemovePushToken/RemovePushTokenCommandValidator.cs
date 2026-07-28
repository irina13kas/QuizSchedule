using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notifications.RemovePushToken
{
    public class RemovePushTokenCommandValidator : AbstractValidator<RemovePushTokenCommand>
    {
        public RemovePushTokenCommandValidator() {
            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Токен обязателен");
        }
    }
}

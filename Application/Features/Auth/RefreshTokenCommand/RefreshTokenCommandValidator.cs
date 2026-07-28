using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.RefreshTokenCommand
{
    public class RefreshTokenCommandValidator: AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.Token)
                .NotNull().WithMessage("Токен обязателен");

            RuleFor(x => x.RefreshToken)
                .NotNull().WithMessage("Токен обязателен");

        }
    }
}

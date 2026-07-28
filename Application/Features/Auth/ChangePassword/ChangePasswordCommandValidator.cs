using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator() {
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Пароль обязателен")
                .MinimumLength(5).WithMessage("Длина пароля должна быть более 5 символов");

            RuleFor(x => x.NewPassword)
                .Equal(m => m.ConfirmPassword).WithMessage("Пароли не совпадают");

            RuleFor(x => x.NewPassword)
                .NotEqual(m => m.OldPassword).WithMessage("Новый пароль такой же как старый");
        }
    }
}

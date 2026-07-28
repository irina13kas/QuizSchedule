using Application.DTOs.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.CompleteFirstLogin
{
    public class CompleteFirstLoginCommandValidator: AbstractValidator<CompleteFirstLoginCommand>
    {
        public CompleteFirstLoginCommandValidator() {
            RuleFor(x => x.VkUrl)
                .NotEmpty().WithMessage("Ссылка на ВК обязательна");

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID обязателен");

            RuleFor(x => x.BirthDay)
                .LessThan(DateTime.Today).WithMessage("Дата рождения не может быть позже текущей")
                .When(u => u.BirthDay.HasValue);
        }
    }
}

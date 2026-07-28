using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FinesHistory.AddFine
{
    public class AddFineCommandValidator: AbstractValidator<AddFineCommand>
    {
        public AddFineCommandValidator()
        {
            RuleFor(x => x.QuizmanId)
                .NotEmpty().WithMessage("Квизмен обязателен");

            RuleFor(x => x.AdminId)
                .NotEmpty().WithMessage("Админ обязателен");

            RuleFor(x => x.GameId)
                .NotEmpty().WithMessage("Игра обязательна");

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Количество обязательно")
                .GreaterThan(0).WithMessage("Штраф должен быть больше 0");


            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Комментарий за что начислен штраф обязателен");
        }
    }
}

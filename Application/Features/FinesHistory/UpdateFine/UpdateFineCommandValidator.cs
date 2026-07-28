using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FinesHistory.UpdateFine
{
    public class UpdateFineCommandValidator : AbstractValidator<UpdateFineCommand>
    {
        public UpdateFineCommandValidator() 
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id штрафа обязателен");

            RuleFor(x => x.QuizmanId)
                .NotEmpty().WithMessage("Квизмен обязателен");

            RuleFor(x => x.AdminId)
                .NotEmpty().WithMessage("Админ обязателен");

            RuleFor(x => x.GameId)
                .NotEmpty().WithMessage("Игра обязательна");

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Количество обязательно");

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Комментарий за что начислен штраф обязателен");
        }
    }
}

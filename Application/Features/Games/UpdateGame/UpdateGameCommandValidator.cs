using Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Games.UpdateGame
{
    public class UpdateGameCommandValidator : AbstractValidator<UpdateGameCommand>
    {
        public UpdateGameCommandValidator() {
            RuleFor(x => x.GameId)
                .NotEmpty().WithMessage("Id Игры обязателен");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя Игры обязательно");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Статус Игры обязателен")
                .IsEnumName(typeof(GameStatus), caseSensitive: false).WithMessage("Не существует такого статуса Игры");

            RuleFor(x => x.ResponsibleAdminId)
                .NotEmpty().WithMessage("Администратор обязателен");

            RuleFor(x => x.BarId)
                .NotEmpty().WithMessage("Бар обязателен");

            RuleFor(x => x.GameStartTime)
                .NotEmpty().WithMessage("Время Игры обязательно")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Время Игры не может быть в прошлом");

            RuleFor(x => x.WorkStartTime)
                .NotEmpty().WithMessage("Время начала смены обязательно")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Время начала смены не может быть в прошлом")
                .LessThan(x => x.GameStartTime).WithMessage("Время начала смены не может быть позже начала Игры");

            RuleFor(x => x.MasterId)
                .NotEmpty().WithMessage("Ведущий обязателен");

            RuleFor(x => x.DjId)
                .NotEmpty().WithMessage("Dj обязателен");

            RuleFor(x => x.PhotographerId)
                .NotEmpty().WithMessage("Фотограф обязателен");
        }
    }
}

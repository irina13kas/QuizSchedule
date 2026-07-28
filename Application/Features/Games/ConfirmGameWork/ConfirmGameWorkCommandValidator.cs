using Application.Common.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Games.ConfirmGameWork
{
    public class ConfirmGameWorkCommandValidator : AbstractValidator<ConfirmGameWorkCommand>
    {
        public ConfirmGameWorkCommandValidator()
        {
            RuleFor(x => x.GameId)
                .NotEmpty().WithMessage("Id Игры обязателен");

            RuleFor(x => x.ExcludeQuizmanIds)
                .Must(list => list.Distinct().Count() == list.Count)
                .WithMessage("В списке есть дубликаты");
        }
    }
}

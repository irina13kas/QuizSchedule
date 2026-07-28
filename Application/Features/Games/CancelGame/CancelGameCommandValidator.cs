using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Games.CancelGame
{
    public class CancelGameCommandValidator : AbstractValidator<CancelGameCommand>
    {
        public CancelGameCommandValidator() {
            RuleFor(x => x.GameId)
                .NotEmpty().WithMessage("Id Игры обязателен");

            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("Причина обязательна");
        }
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Games.PublishGame
{
    public class PublishGameCommandValidator : AbstractValidator<PublishGameCommand>
    {
        public PublishGameCommandValidator() {
            RuleFor(x => x.GameId)
                .NotEmpty().WithMessage("Id Игры обязателен");
        }
    }
}

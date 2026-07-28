using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Games.GetGame
{
    public class GetGameQueryValidator : AbstractValidator<GetGameQuery>
    {
        public GetGameQueryValidator() {
            RuleFor(x => x.GameId)
                .NotEmpty().WithMessage("Id Игры обязателен");
        }
    }
}

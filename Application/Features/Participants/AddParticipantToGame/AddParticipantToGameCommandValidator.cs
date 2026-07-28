using Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Participants.AddParticipantToGame
{
    public class AddParticipantToGameCommandValidator : AbstractValidator<AddParticipantToGameCommand>
    {
        public AddParticipantToGameCommandValidator()
        {
            RuleFor(x => x.GameId)
                .NotEmpty().WithMessage("Id Игры обязателен");

            RuleFor(x => x.QuizmanId)
                .NotEmpty().WithMessage("Id Квизмена обязателен");

            RuleFor(x => x.Role)
                .Must(role => string.IsNullOrEmpty(role) || Enum.TryParse<ParticipantRole>(role, true, out _))
                .WithMessage("Недопустимый формат для роли квизмена на игру")
                .When(x => x.Role != null);

            RuleFor(x => x.IsActive)
                .NotEmpty().WithMessage("Статус квизмена на игре обязателен");

            RuleFor(x => x.IsFullShift)
                .NotEmpty().WithMessage("Указание полной/неполной смены обязательно");
        }
    }
}

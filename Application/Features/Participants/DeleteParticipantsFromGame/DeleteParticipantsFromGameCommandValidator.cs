using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Participants.DeleteParticipantsFromGame
{
    public class DeleteParticipantsFromGameCommandValidator : AbstractValidator<DeleteParticipantFromGameCommand>
    {
        public DeleteParticipantsFromGameCommandValidator() {
            RuleFor(x => x.ParticipantId)
                .NotEmpty().WithMessage("Id участника игры обязателен");
        }
    }
}

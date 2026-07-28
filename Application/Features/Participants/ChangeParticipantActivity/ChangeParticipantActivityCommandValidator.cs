using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Participants.ChangeParticipantActivity
{
    public class ChangeParticipantActivityCommandValidator : AbstractValidator<ChangeParticipantActivityCommand>
    {
        public ChangeParticipantActivityCommandValidator() {
            RuleFor(x => x.ParticipantId)
                .NotEmpty().WithMessage("Id участника смены обязателен");
        }
    }
}

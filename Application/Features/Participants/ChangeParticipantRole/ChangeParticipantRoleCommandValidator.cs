using Domain.Enums;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Participants.ChangeParticipantRole
{
    public class ChangeParticipantRoleCommandValidator: AbstractValidator<ChangeParticipantRoleCommand>
    {
        public ChangeParticipantRoleCommandValidator() {
            RuleFor(x => x.ParticipantId)
                .NotEmpty().WithMessage("Id участника смены обязателен");

            RuleFor(x => x.RoleName)
                .Must(role => string.IsNullOrEmpty(role) || Enum.TryParse<ParticipantRole>(role, true, out _))
                .WithMessage("Недопустимый формат для роли квизмена на игру")
                .When(x => x.RoleName != null);
        }
    }
}

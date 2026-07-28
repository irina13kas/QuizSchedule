using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Participants;
using Domain.Enums;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Participants.ChangeShiftType
{
    public class ChangeShiftTypeCommandValidator : AbstractValidator<ChangeShiftTypeCommand>
        { 
        public ChangeShiftTypeCommandValidator() {
            RuleFor(x => x.ParticipantId)
                .NotEmpty().WithMessage("Id участника игры обязателен");
        }
    }
}

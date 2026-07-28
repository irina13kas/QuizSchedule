using Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SmartSchedule.UpdateSmart
{
    public class UpdateSmartCommandValidator : AbstractValidator<UpdateSmartCommand>
    {
        public UpdateSmartCommandValidator() {
            RuleFor(x => x.SmartId)
                .NotEmpty().WithMessage("Id дня Smarta квизмена обязателен");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Статус квизмена в Smart не может быть нулевым")
                .IsEnumName(typeof(SmartStatus), true).WithMessage("Значение не из списка допустимых значений");
        }
    }
}

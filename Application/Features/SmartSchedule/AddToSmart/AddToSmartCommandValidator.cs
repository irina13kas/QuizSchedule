using Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SmartSchedule.AddToSmart
{
    public class AddToSmartCommandValidator : AbstractValidator<AddToSmartCommand>
    {
        public AddToSmartCommandValidator()
        {

            RuleFor(x => x.Date)
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Нельзя менять Smart на прошедшую дату");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Статус поле обязательное")
                .IsEnumName(typeof(SmartStatus), caseSensitive: false).WithMessage("");
        }
    }
}

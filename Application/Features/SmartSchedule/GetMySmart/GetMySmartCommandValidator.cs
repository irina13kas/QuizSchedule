using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SmartSchedule.GetMySmart
{
    public class GetMySmartCommandValidator : AbstractValidator<GetMySmartCommand>
    {
        public GetMySmartCommandValidator() {
            RuleFor(x => x.DateFrom)
                .LessThanOrEqualTo(x => x.DateTo)
                .WithMessage("Дата От не должна быть позже даты До")
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage("Дата От не должна быть позже сегодняшней");

            RuleFor(x => x.DateTo)
                .GreaterThanOrEqualTo(x => x.DateFrom)
                .WithMessage("Дата До не должна быть раньше даты От")
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage("Дата До не должна быть раньше сегодняшней");
        }
    }
}

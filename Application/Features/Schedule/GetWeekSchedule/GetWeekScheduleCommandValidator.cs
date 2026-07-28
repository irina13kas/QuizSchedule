using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Schedule.GetWeekSchedule
{
    public class GetWeekScheduleCommandValidator : AbstractValidator<GetWeekScheduleCommand>
    {
        public GetWeekScheduleCommandValidator() {
            RuleFor(x => x.DateFrom)
                .LessThanOrEqualTo(x => x.DateTo).WithMessage("Начальная дата не может быть больше конечной")
                .LessThanOrEqualTo(x => DateTime.Today).WithMessage("Дата не может быть позже текущей");

            RuleFor(x => x.DateTo)
                .GreaterThanOrEqualTo(x => x.DateFrom).WithMessage("КОнечная дата не может быть меньше начальной")
                .LessThanOrEqualTo(x => DateTime.Today).WithMessage("Дата не может быть позже текущей");
        }
    }
}

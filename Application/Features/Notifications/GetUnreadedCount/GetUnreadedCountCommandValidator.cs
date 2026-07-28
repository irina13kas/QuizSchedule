using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notifications.GetUnreadedCount
{
    public class GetUnreadedCountCommandValidator : AbstractValidator<GetUnreadedCountCommand>
    {
        public GetUnreadedCountCommandValidator()
        {
            RuleFor(x => x.FromDate)
               .LessThanOrEqualTo(x => x.ToDate).When(x => x.FromDate.HasValue && x.ToDate.HasValue).WithMessage("Начальная дата не может быть больше конечной")
               .LessThanOrEqualTo(x => DateTime.Today).When(x => x.FromDate.HasValue).WithMessage("Дата не может быть позже текущей");

            RuleFor(x => x.ToDate)
                .GreaterThanOrEqualTo(x => x.FromDate).When(x => x.FromDate.HasValue && x.ToDate.HasValue).WithMessage("КОнечная дата не может быть меньше начальной")
                .LessThanOrEqualTo(x => DateTime.Today).When(x => x.ToDate.HasValue).WithMessage("Дата не может быть позже текущей");
        }
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FinesHistory.GetAllFines
{
    public class GetMyFineHistoryQueryValidator : AbstractValidator<GetMyFineHistoryQuery>
    {
        public GetMyFineHistoryQueryValidator() {
            RuleFor(x => x.FromDate)
                .LessThanOrEqualTo(x => x.ToDate).When(x => x.FromDate.HasValue && x.ToDate.HasValue).WithMessage("Начальная дата не может быть больше конечной")
                .LessThanOrEqualTo(x => DateTime.Today).When(x => x.FromDate.HasValue).WithMessage("Дата не может быть позже текущей");

            RuleFor(x => x.ToDate)
                .GreaterThanOrEqualTo(x => x.FromDate).When(x => x.FromDate.HasValue && x.ToDate.HasValue).WithMessage("КОнечная дата не может быть меньше начальной")
                .LessThanOrEqualTo(x => DateTime.Today)
                .When(x => x.ToDate.HasValue).WithMessage("Дата не может быть позже текущей");

            RuleFor(x => x.Page)
                .NotEmpty().WithMessage("Номер страницы не может быть пустым")
                .GreaterThan(0).WithMessage("Номер страницы должен быть больше 0");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Не может быть меньше 0")
                .NotEmpty().WithMessage("Не может быть пустым");
        }
    }
}

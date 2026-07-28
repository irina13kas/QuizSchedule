using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PointsHistory.GetQuizemanPointsHistory
{
    public class GetQuizmanPointsHistoryCommandValidator : AbstractValidator<GetQuizmanPointsHistoryCommand>
    {
        public GetQuizmanPointsHistoryCommandValidator()
        {
            RuleFor(x => x.QuizmanId)
                .NotEmpty().WithMessage("Id Квизмена обязателен");

            RuleFor(x => x.DateFrom)
                .LessThanOrEqualTo(x => x.DateTo)
                .When(x => x.DateFrom.HasValue && x.DateTo.HasValue).WithMessage("Дата От не должна быть позже даты До")
                .LessThanOrEqualTo(DateTime.Today)
                .When(x => x.DateFrom.HasValue && x.DateTo.HasValue).WithMessage("Дата От не должна быть позже сегодняшней");

            RuleFor(x => x.DateTo)
                .GreaterThanOrEqualTo(x => x.DateFrom)
                .When(x => x.DateFrom.HasValue && x.DateTo.HasValue).WithMessage("Дата До не должна быть раньше даты От")
                .LessThanOrEqualTo(DateTime.Today)
                .When(x => x.DateFrom.HasValue && x.DateTo.HasValue).WithMessage("Дата До не должна быть раньше сегодняшней");

            RuleFor(x => x.Page)
                .GreaterThan(0).WithMessage("Не может быть меньше 0")
                .NotEmpty().WithMessage("Не может быть пустым");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Не может быть меньше 0")
                .NotEmpty().WithMessage("Не может быть пустым");

        }
    }
}

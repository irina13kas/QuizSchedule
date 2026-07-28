using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PointsHistory.DeletePoints
{
    public class DiscardPointsCommandValidator : AbstractValidator<DiscardPointsCommand>
    {
        public DiscardPointsCommandValidator() 
        {
            RuleFor(x => x.QuizmanId)
                .NotEmpty().WithMessage("Id Квизмена обязателен");

            RuleFor(x => x.AdminId)
                .NotEmpty().WithMessage("Id Админа обязателен");

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Количество очков обязательно")
                .GreaterThan(0).WithMessage("Количество очков должно быть больше 0");

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Причина списания баллов обязательна");
        }
    }
}

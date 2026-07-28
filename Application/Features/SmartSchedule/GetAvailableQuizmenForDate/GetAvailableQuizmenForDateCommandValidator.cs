using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SmartSchedule.GetAvailableQuizmen
{
    public class GetAvailableQuizmenForDateCommandValidator : AbstractValidator<GetAvailableQuizmenForDateCommand>
    {
        public GetAvailableQuizmenForDateCommandValidator() {
            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Дата не может быть нулевой");
        }
    }
}

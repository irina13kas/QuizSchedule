using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Schedule.GetGamesList
{
    public class GetDailyScheduleCommandValidator : AbstractValidator<GetDailyScheduleCommand>
    {
        public GetDailyScheduleCommandValidator() {
            
        }
    }
}

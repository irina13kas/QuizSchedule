using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PointsHistory.DeletePoints
{
    public class DeletePointsCommandValidator : AbstractValidator<DeleteSmartCommand>
    {
        public DeletePointsCommandValidator() 
        {
            RuleFor(x => x.PointsId)
                .NotEmpty().WithMessage("Id записи об очках обязателен");
        }   
    }
}

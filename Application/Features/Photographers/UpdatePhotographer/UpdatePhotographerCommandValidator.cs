using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Photographers.UpdatePhotographer
{
    public class UpdatePhotographerCommandValidator : AbstractValidator<UpdatePhotographerCommand>
    {
        public UpdatePhotographerCommandValidator()
        {
            RuleFor(x => x.PhotographerId)
                .NotEmpty().WithMessage("Id Фотографа обязателен");
        }
    }
}

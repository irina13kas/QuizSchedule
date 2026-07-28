using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Photographers.AddPhotographer
{
    public class AddPhotographerCommandValidator : AbstractValidator<AddPhotographerCommand>
    {
        public AddPhotographerCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя Фотографа обязательно");
        }
    }
}

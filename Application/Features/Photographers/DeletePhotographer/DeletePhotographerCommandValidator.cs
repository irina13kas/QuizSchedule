using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Photographers.DeletePhotographer
{
    public class DeletePhotographerCommandValidator : AbstractValidator<DeletePhotographerCommand>
    {
        public DeletePhotographerCommandValidator()
        {
            RuleFor(x => x.PhotographerId)
                .NotEmpty().WithMessage("Id Фотографа обязателен");
        }
    }
}

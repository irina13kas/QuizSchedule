using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Photographers.GetPhotographer
{
    public class GetPhotographerQueryValidator : AbstractValidator<GetPhotographerQuery>
    {
        public GetPhotographerQueryValidator()
        {
            RuleFor(x => x.PhotographerId)
                .NotEmpty().WithMessage("Id Фотографа обязателен");
        }
    }
}

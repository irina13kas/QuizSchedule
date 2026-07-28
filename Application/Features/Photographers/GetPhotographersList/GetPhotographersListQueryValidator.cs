using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Photographers.GetPhotographersList
{
    public class GetPhotographersListQueryValidator : AbstractValidator<GetPhotographersListQuery>
    {
        public GetPhotographersListQueryValidator() { }
    }
}

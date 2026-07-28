using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Bars.GetBarsList
{
    public class GetBarsListQueryValidator : AbstractValidator<GetBarsListQuery>
    {
        public GetBarsListQueryValidator() { }
    }
}

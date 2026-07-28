using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Masters.GetMaster
{
    public class GetMasterQueryValidator : AbstractValidator<GetMasterQuery>
    {
        public GetMasterQueryValidator() {
            RuleFor(x => x.MasterId)
                .NotEmpty().WithMessage("Id Ведущего обязателен");
        }
    }
}

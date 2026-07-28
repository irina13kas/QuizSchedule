using Application.DTOs.Replacements;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Replacements.DeleteReplacement
{
    public class DeleteReplacementCommand : IRequest<ChangeStateReplacement>
    {
        public Guid ReplacementId { get; set; }
    }
}

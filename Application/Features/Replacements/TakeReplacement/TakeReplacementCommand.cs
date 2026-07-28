using Application.DTOs.Replacements;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Replacements.TakeReplacement
{
    public class TakeReplacementCommand: IRequest<TakeReplacementResponse>
    {
        public Guid ReplacementId { get; set; }
    }
}

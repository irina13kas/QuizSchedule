using Application.DTOs.Replacements;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Replacements.UpdateReplacement
{
    public class UpdateReplacementCommand : IRequest<UpdateReplacementResponse>
    {
        public Guid ReplacementId { get; set; }
        public string? Comment { get; set; }
        public bool IsFullShift { get; set; }
    }
}

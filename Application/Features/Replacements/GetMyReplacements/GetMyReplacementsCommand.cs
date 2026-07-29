using Application.DTOs.Replacements;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Replacements.GetReplacements
{
   public class GetMyReplacementsCommand : IRequest<GetReplacementsResponse>
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

    }
}

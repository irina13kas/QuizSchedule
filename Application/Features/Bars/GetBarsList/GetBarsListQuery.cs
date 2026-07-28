using Application.DTOs.Bars;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Bars.GetBarsList
{
    public class GetBarsListQuery : IRequest<BarListResponse>
    {
        public bool? IsActive { get; set; }
    }
}

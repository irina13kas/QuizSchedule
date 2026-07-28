using Application.DTOs.Bars;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Bars.GetBar
{
    public class GetBarQuery : IRequest<BarResponse>
    {
        public Guid BarId { get; set; }
    }
}

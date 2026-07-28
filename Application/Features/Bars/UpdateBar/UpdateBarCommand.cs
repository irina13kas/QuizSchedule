using Application.DTOs.Bars;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Bars.UpdateBar
{
    public class UpdateBarCommand : IRequest<BarResponse>
    {
        public Guid BarId { get; set; }
        public string? NewBarName { get; set; }
        public string? NewAddress { get; set; }
        public int? NewCapacity { get; set; }
    }
}

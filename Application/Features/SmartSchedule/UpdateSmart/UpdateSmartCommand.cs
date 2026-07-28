using Application.DTOs.Smart;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SmartSchedule.UpdateSmart
{
    public class UpdateSmartCommand : IRequest<SmartResponse>
    {
        public Guid SmartId {get; set;}
        public string Status { get; set; } = string.Empty;
        public string? Comment { get; set; }
    }
}

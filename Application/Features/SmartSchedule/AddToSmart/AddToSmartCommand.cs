using Application.DTOs.Smart;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SmartSchedule.AddToSmart
{
    public class AddToSmartCommand : IRequest<SmartResponse>
    {
        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Comment { get; set; }
    }
}

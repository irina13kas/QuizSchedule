using Application.DTOs.Smart;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SmartSchedule.GetMySmart
{
    public class GetMySmartCommand : IRequest<SmartDaysForQuizmanResponse>
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}

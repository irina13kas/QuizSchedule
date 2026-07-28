using Application.Common.Interfaces;
using Application.DTOs.Smart;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SmartSchedule.DeleteFromSmart
{
    public class DeleteFromSmartCommand : IRequest<ChangeStateSmartResponse>
    {
        public Guid SmartId { get; set; }
    }
}

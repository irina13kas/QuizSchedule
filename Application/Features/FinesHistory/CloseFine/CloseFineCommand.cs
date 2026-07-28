using Application.DTOs.Fines;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FinesHistory.CloseFine
{
    public class CloseFineCommand : IRequest<FineChangeStateResponse> 
    {
        public Guid FineId { get; set; }
    }
}

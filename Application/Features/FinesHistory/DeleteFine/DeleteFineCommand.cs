using Application.DTOs.Fines;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FinesHistory.DeleteFine
{
    public class DeleteFineCommand : IRequest<FineChangeStateResponse>
    {
        public Guid Id { get; set;}
    }
}

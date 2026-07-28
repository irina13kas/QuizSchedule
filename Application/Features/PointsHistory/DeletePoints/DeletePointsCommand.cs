using Application.DTOs.Points;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PointsHistory.DeletePoints
{
    public class DeleteSmartCommand : IRequest<PointsChangeStateResponse>
    {
        public Guid PointsId { get; set; }
    }
}

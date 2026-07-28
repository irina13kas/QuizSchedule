using Application.DTOs.Djs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Djs.UpdateDj
{
    public class UpdateDjCommand : IRequest<DjResponse>
    {
        public Guid DjId { get; set; }
        public string? NewName { get; set; }
    }
}

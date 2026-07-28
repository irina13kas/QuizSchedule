using Application.DTOs.Djs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Djs.GetDj
{
    public class GetDjQuery : IRequest<DjResponse>
    {
        public Guid DjId { get; set; }
    }
}

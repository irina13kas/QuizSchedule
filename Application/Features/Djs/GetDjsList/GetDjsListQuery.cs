using Application.DTOs.Djs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Djs.GetDjsList
{
    public class GetDjsListQuery : IRequest<DjsListResponse>
    {
        public bool? IsActive { get; set; }
    }
}

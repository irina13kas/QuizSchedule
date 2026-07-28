using Application.DTOs.Masters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Masters.GetMastersList
{
    public class GetMastersListQuery : IRequest<MastersListResponse>
    {
        public bool? IsActive { get; set; }
    }
}

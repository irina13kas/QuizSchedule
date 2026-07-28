using Application.DTOs.Masters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Masters.UpdateMaster
{
    public class UpdateMasterCommand : IRequest<MasterResponse>
    {
        public Guid MasterId { get; set; }
        public string? NewName { get; set; }
    }
}

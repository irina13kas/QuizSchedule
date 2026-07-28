using Application.DTOs.Masters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Masters.DeleteMaster
{
    public class DeleteMasterCommand : IRequest<ChangeStateMasterResponse>
    {
        public Guid MasterId { get; set; }
    }
}

using Application.DTOs.Masters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Masters.AddMaster
{
    public class AddMasterCommand : IRequest<MasterResponse>
    {
        public string Name { get; set; } = string.Empty;
    }
}

using Application.DTOs.Djs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Djs.AddDj
{
    public class AddDjCommand : IRequest<DjResponse>
    {
        public string Name { get; set; } = string.Empty;
    }
}

using Application.DTOs.Photographers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Photographers.AddPhotographer
{
    public class AddPhotographerCommand : IRequest<PhotographerResponse>
    {
        public string Name { get; set; } = string.Empty;
    }
}

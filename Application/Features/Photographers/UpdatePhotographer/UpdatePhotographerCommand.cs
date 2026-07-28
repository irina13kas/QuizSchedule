using Application.DTOs.Photographers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Photographers.UpdatePhotographer
{
    public class UpdatePhotographerCommand : IRequest<PhotographerResponse>
    {
        public Guid PhotographerId { get; set; }
        public string? NewName { get; set; }
    }
}

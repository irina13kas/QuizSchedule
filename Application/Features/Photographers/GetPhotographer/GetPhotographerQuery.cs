using Application.DTOs.Photographers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Photographers.GetPhotographer
{
    public class GetPhotographerQuery : IRequest<PhotographerResponse>
    {
        public Guid PhotographerId { get; set; }
    }
}

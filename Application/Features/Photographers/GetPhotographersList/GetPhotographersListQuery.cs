using Application.DTOs.Photographers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Photographers.GetPhotographersList
{
    public class GetPhotographersListQuery : IRequest<PhotographersListResponse>
    {
        public bool? IsActive { get; set; }
    }
}

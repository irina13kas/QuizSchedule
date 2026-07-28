using Application.DTOs.Profiles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Profiles.UpdatePhoto
{
    public class UpdatePhotoCommand : IRequest<UpdatePhotoResponse>
    {
        public string? PhotoUrl { get; set; }
    }
}

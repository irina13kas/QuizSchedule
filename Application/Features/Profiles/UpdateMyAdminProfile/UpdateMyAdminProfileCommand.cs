using Application.DTOs.Profiles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Profiles.UpdateMyAdminProfile
{
    public class UpdateMyAdminProfileCommand : IRequest<AdminProfileResponse>
    {
        public string? AdminName { get; set; } = string.Empty;
        public DateTime? BirthDay { get; set; }
        public string? PhotoUrl { get; set; }
        public string? VkUrl { get; set; }
        public string? DaysOff { get; set; }
    }
}

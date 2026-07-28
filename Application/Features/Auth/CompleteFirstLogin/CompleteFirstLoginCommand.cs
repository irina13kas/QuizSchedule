using Application.DTOs.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.CompleteFirstLogin
{
    public class CompleteFirstLoginCommand : IRequest<CompleteFirstLoginResponse>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? VkUrl { get; set; }
        public string? PhotoUrl { get; set; }
        public DateTime? BirthDay { get; set; }
    }
}

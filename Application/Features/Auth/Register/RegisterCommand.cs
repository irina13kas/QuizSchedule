using Application.DTOs.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Register
{
    public class RegisterCommand : IRequest<RegisterResponse>
    {
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string VKUrl { get; set; } = string.Empty;
    }
}

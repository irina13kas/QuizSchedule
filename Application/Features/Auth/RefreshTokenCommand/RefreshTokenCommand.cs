using Application.DTOs.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.RefreshTokenCommand
{
    public class RefreshTokenCommand : IRequest<LoginResponse>
    {
        public string Token {get; set;} = string.Empty;
        public string RefreshToken {get; set;} = string.Empty;
    }
}

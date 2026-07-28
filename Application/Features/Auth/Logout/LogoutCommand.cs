using Application.DTOs.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Logout
{
    public class LogoutCommand: IRequest<AuthChangeStateResponse>
    {
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        Guid? UserId { get;}
        string? Login { get;}
        string? Role { get;}
        bool IsAuthenticated { get;}

        string? ClientIp { get; }
    }
}

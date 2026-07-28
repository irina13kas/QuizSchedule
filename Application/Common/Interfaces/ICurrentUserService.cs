using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public class ICurrentUserService
    {
        public Guid? UserId { get; set; }
        public string? Login { get; set; }
        public string? Role { get; set; }
        public bool IsAuthenticated { get; set;}

        public string? ClientIp { get; }
    }
}

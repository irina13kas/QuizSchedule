using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth
{
    public class RegisterResponse
    {
        public Guid UserId { get; set; }
        public string TempPassword { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty ;
        public string VKUrl { get; set; } = string.Empty;
    }
}

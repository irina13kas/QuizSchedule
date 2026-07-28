using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth
{
    public class LoginResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public Guid UserId {get; set;}
        public string Role { get; set; } = string.Empty;
        public DateTime AccessTokenExpiresAt { get; init; }
        public string RefreshToken { get; set; } = string.Empty;

    }
}

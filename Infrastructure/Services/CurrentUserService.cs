using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? UserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor.HttpContext?
                    .User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userIdClaim))
                    return null;

                return Guid.TryParse(userIdClaim, out var userId) ? userId : null;
            }
        }

        public string? Login => 
            _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;

        public string? Role =>
            _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;

        public bool IsAuthenticated =>
            _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

        public string? ClientIp
        {
            get
            {
                var httpContext = _httpContextAccessor.HttpContext;

                if (httpContext == null) return null;

                var forwardedFor = httpContext.Request.Headers["X-Forwarded-For"]
                    .FirstOrDefault();
                if(!string.IsNullOrEmpty(forwardedFor))
                    return forwardedFor.Split(',')[0].Trim();

                return httpContext.Connection.RemoteIpAddress?.ToString();
            }
        }
    }
}

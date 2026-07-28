using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth
{
    public class CompleteFirstLoginResponse
    {
        public string Name { get; set; } = string.Empty;
        public string? PhotoUrl { get; set; }
        public string VkUrl { get; set; } = string.Empty;
        public DateTime? BirthDay { get; set; }
    }
}

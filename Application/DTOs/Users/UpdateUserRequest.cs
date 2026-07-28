using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Users
{
    public class UpdateUserRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? PhotoUrl { get; set; }
        public DateTime? BirthDay { get; set; }
    }
}

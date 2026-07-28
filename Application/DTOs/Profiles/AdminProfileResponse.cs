using Application.DTOs.Games;
using Application.DTOs.Replacements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Profiles
{
    public class AdminProfileResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime? BirthDay { get; set; }
        public string VkUrl { get; set; } = string.Empty;
        public string? DaysOff { get; set; }
        public string? Photo { get; set; }
    }
}

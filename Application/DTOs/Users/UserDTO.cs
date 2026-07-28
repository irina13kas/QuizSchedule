using Application.DTOs.Notifications;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Users
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string? PhotoUrl { get; set; }
        public string Name { get; set; }
        public string VkUrl { get; set; } = string.Empty;
        public DateTime? BirthDay { get; set; }
        public bool IsFirstEnterFinished { get; set; }
        public bool IsDeleted { get; set; }
        public string Role { get; set; } = string.Empty;

        public List<NotificationDTO> Notifications { get; set; } = new();
    }
}

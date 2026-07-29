 using Application.DTOs.Participants;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Games
{
    public class GameResponseForParticipants
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public Guid ResponsibleAdminId { get; set; }
        public string ResponsibleAdminName { get; set; } = string.Empty;
        public Guid BarId { get; set; }
        public string BarName { get; set; } = string.Empty;
        public string? Partner { get; set; }
        public DateTime GameTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ParticipantsResponse> Participants { get; set; } = new();
    }
}

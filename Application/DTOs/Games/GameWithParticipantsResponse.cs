using Application.DTOs.Participants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Games
{
    public class GameWithParticipantsResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public Guid? ResponsibleAdminId { get; set; }
        public string? ResponsibleAdminName { get; set; }
        public Guid? BarId { get; set; }
        public string? BarName { get; set; }
        public string? Partner { get; set; }
        public DateTime GameStartTime { get; set; }
        public DateTime WorkStartTime { get; set; }
        public Guid? MasterId { get; set; }
        public string? MasterName { get; set; }
        public Guid? DjId { get; private set; }
        public string? DjName { get; set; }
        public Guid? PhotographerId { get; private set; }
        public string? PhotographerName { get; set; }
        public List<ParticipantsResponse> Participants { get; set; } = new();
    }
}

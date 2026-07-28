using Application.DTOs.Participants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Games
{
    public class UpdateGameRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public Guid ResponsibleAdminId { get; set; }
        public Guid BarId { get; set; }
        public string? Partner { get; set; }
        public DateTime GameTime { get; set; }
        public List<ParticipantsResponse> Participants { get; set; } = new();
    }
}

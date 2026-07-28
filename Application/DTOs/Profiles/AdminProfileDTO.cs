using Application.DTOs.Fines;
using Application.DTOs.Games;
using Application.DTOs.Points;
using Application.DTOs.Replacements;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Profiles
{
    public class AdminProfileDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set;} = string.Empty;
        public string? DaysOff { get; set; }

        public List<GameResponseForParticipants> Games { get; set; } = new();

        public List<ReplacementResponse> TakenReplacements { get; set; } = new();
    }
}

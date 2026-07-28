using Application.DTOs.Fines;
using Application.DTOs.Participants;
using Application.DTOs.Points;
using Application.DTOs.Replacements;
using Application.DTOs.Smart;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Profiles
{
    public class QuizmanProfileResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string VkUrl { get; set; } = string.Empty;
        public string? PhotoUrl { get; set; }
        public decimal WorkShift { get; set; }
        public decimal? Points { get; set; }
        public int? Fines { get; set; }
    }
}

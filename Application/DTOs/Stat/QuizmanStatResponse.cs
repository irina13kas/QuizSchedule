using Application.DTOs.Fines;
using Application.DTOs.Participants;
using Application.DTOs.Points;
using Application.DTOs.Profiles;
using Application.DTOs.Replacements;
using Application.DTOs.Smart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Stat
{
    public class QuizmanStatResponse : QuizmanProfileResponse
    {
        public List<SmartResponse> Smart { get; set; } = new();
        public List<ParticipantsResponse> Participants { get; set; } = new();
        public List<ReplacementResponse> Replacements { get; set; } = new();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Games
{
    public class GetGameListResponse
    {
        public List<GameResponseForParticipants> Games { get; set; } = new();
        public int TotalCount { get; set;}
        public int Page { get; set;}
        public int PageSize { get; set; }
        public int TotalPages => (int) Math.Ceiling((double) TotalCount / PageSize);
        
    }
}

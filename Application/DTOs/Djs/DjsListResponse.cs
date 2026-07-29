using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Djs
{
    public class DjsListResponse
    {
        public List<DjResponse> Djs { get; set; } = new();
    }
}

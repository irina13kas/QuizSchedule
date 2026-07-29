using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Photographers
{
    public class PhotographersListResponse
    {
        public List<PhotographerResponse> Photographers { get; set; } = new();
    }
}

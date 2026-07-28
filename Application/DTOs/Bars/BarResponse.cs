using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Bars
{
    public class BarResponse
    {
        public Guid BarId { get; set; }
        public string BarName {get; set;} = string.Empty;
        public string Address { get; set;} = string.Empty;
        public int? Capacity { get; set;}
        public bool IsActive { get; set; }
    }
}

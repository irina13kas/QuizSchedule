using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class NotBusinessSuitableException: Exception
    {
        public NotBusinessSuitableException() : base("Противоречие бизнес правилам") { }

        public NotBusinessSuitableException(string message) : base(message) { }

        public NotBusinessSuitableException(string message, Exception innerException)
        : base(message, innerException) { }
    }
}

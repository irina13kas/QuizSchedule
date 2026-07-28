using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException() : base("Доступ запрещен") { }

        public ForbiddenException(string message) : base(message) { }

        public ForbiddenException(string message, Exception innerException)
        : base(message, innerException) { }
        public ForbiddenException(string entityName, object key)
        : base($"Доступ к сущности \"{entityName}\" с ключом ({key}) запрещен.") { }
    }
}

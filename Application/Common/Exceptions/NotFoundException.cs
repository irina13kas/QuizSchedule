using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base("Сущность не найдена") { }

        public NotFoundException(string message) : base(message){}

        public NotFoundException(string message, Exception innerException)
        : base(message, innerException) {}
        public NotFoundException(string entityName, object key)
        : base($"Сущность \"{entityName}\" с ключом ({key}) не найдена."){}
    }
}

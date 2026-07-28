using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class ExistInDBException : Exception
    {
        public ExistInDBException() : base("Уже есть сущность с таким ID в БД") { }

        public ExistInDBException(string message) : base(message) { }

        public ExistInDBException(string message, Exception innerException)
        : base(message, innerException) { }
    }
}

using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int WorkFactor = 11;
        public string Hash(string password)
        {
            if(string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("Пароль не может быть пустым");

            return BCrypt.Net.BCrypt.EnhancedHashPassword(password, WorkFactor);
        }
        public bool Verify(string password, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            if (string.IsNullOrWhiteSpace(hashedPassword))
                return false;

            try
            {
                return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
            }
            catch
            {
                return false;
            }
        }
    }
}

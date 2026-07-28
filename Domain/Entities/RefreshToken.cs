using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RefreshToken : BaseEntity
    {
        public Guid UserId { get; private set; }
        public string Token { get; private set; } = string.Empty;
        public DateTime ExpiresAt { get; private set; }
        public string? CreatedByIp { get; private set; }
        //Дата отзыва токена
        public DateTime? RevokedAt { get; private set; }
        //Ip-адрес, с которого был отозван токен
        public string? RevokedByIp { get; private set; }
        //Id токена, которым этот был заменен
        public Guid? ReplacedByTokenId { get; private set; }
        public bool IsRevoked => RevokedAt.HasValue;
        public bool IsExpired => DateTime.UtcNow <= ExpiresAt;
        public bool IsActive => (!IsExpired && !IsRevoked);

        public virtual User User { get; private set; } = null!;

        protected RefreshToken() { }

        public RefreshToken(
            Guid userId,
            string token,
            DateTime expiresAt,
            string? createdByIp = null)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException("UserId не может быть нулевым");

            if(string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException("Token не может быть нулевым");

            UserId = userId;
            Token = token;
            ExpiresAt = expiresAt;
            CreatedByIp = createdByIp;
            CreatedAt = DateTime.UtcNow;
        }

        public void Revoke(string? revokedByIp = null, Guid? replacedById = null)
        {
            if (IsRevoked)
                throw new InvalidOperationException("Токен уже отозван");

            RevokedAt = DateTime.UtcNow;
            RevokedByIp = revokedByIp;
            ReplacedByTokenId = replacedById;
        }
    }
}

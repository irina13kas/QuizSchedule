using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        Task<RefreshToken?> GetByTokenAsync(
            string refreshToken, 
            CancellationToken cancellationToken);

        Task<List<RefreshToken>> GetActiveByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default);

        Task RevokeAllByUserAsync(
            Guid userId,
            string? revokedByIp = null,
            CancellationToken cancellationToken = default);

        Task RevokeExpiredOlderThanAsync(
            DateTime cutoffDate,
            CancellationToken cancellationToken = default);
    }
}

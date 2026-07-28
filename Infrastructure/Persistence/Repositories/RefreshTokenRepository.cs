using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(AppDbContext context): base(context) { 
        }

        public async Task<RefreshToken?> GetByTokenAsync(
            string refreshToken,
            CancellationToken cancellationToken)
        {
            return await _context.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.Token == refreshToken, cancellationToken);
        }

        public async Task<List<RefreshToken>> GetActiveByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            return await _context.RefreshTokens
                .Include(rt => rt.User)
                .Where(rt => rt.UserId == userId 
                && rt.RevokedAt == null
                && rt.ExpiresAt >= DateTime.UtcNow)
                .OrderByDescending(rt => rt.CreatedAt)
                .ToListAsync();
        }

        public async Task RevokeAllByUserAsync(
            Guid userId,
            string? revokedByIp = null,
            CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;

            await _context.RefreshTokens
                .Where(rt => rt.UserId == userId && rt.RevokedAt == null)
                .ExecuteUpdateAsync(
                    setters => setters
                        .SetProperty(rt => rt.RevokedAt, now)
                        .SetProperty(rt => rt.RevokedByIp, revokedByIp),
                    cancellationToken);


        }

        public async Task RevokeExpiredOlderThanAsync(
            DateTime cutoffDate,
            CancellationToken cancellationToken = default)
        {
            await _context.RefreshTokens
                .Where(rt => rt.ExpiresAt < cutoffDate)
                .ExecuteDeleteAsync(cancellationToken);
        }
    }
}

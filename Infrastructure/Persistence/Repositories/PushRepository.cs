using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class PushRepository : Repository<PushToken>, IPushRepository
    {
        public PushRepository(AppDbContext context): base(context) { }

        public async Task<PushToken?> GetTokenAsync(
            string token,
            CancellationToken cancellationToken = default)
        {
            return await _context.PushTokens
                .FirstOrDefaultAsync(t => t.Token == token, cancellationToken);
        }

        public async Task<List<PushToken>> GetAllActiveTokensAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            return await _context.PushTokens
                .Where(p => p.UserId == userId && p.IsActive)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public Task DeactivateAllByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            return _context.PushTokens
                .Where(pt => pt.IsActive && pt.UserId == userId)
                .ExecuteUpdateAsync(
                    setter => setter.SetProperty(pt => pt.IsActive, false),
                    cancellationToken);
        }
    }
}

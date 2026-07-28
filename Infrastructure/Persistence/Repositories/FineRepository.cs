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
    public class FineRepository : Repository<Fine>, IFineRepository
    {
        public FineRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Fine>> GetFineHistoryAsync(
            Guid quizemanId,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int skip = 0,
            int take = 20,
            CancellationToken cancellationToken = default
            )
        {

            var query = _context.FinesHistory
                .Include(f => f.Quizman).ThenInclude(u => u.User)
                .Include(f => f.Game)
                .Include(f => f.Admin).ThenInclude(u => u.User)
                .Where(f => f.QuizmanId == quizemanId);

            if (fromDate.HasValue)
                query = query.Where(f => f.Game.GameStartTime >= fromDate);

            if (toDate.HasValue)
                query = query.Where(f => f.Game.GameStartTime <= toDate);


            return await query
                .OrderByDescending(f => f.Game.GameStartTime)
                .Skip(skip)
                .Take(take)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> CountByQuizemanAsync(
            Guid quizemanId,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            CancellationToken cancellationToken = default)
        {
            var query = _context.FinesHistory
                .Where(f => f.QuizmanId == quizemanId);

            if (fromDate.HasValue)
                query = query.Where(f => f.Game.GameStartTime >= fromDate);

            if (toDate.HasValue)
                query = query.Where(f => f.Game.GameStartTime <= toDate);

            return await query.CountAsync(cancellationToken);
        }
    }
}

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
    public class PointsRepository : Repository<Point>, IPointsRepository
    {
        public PointsRepository(AppDbContext context): base(context) { }
        public async Task<List<Domain.Entities.Point>> GetFilteredPointsHistoryAsync(
            Guid quizmanId,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            int skip = 0,
            int take = 20,
            CancellationToken cancellationToken = default)
        {
            var query = _context.PointsHistory
                .Include(p => p.Game)
                .Include(p => p.Admin).ThenInclude(x => x.User)
                .Include(p => p.Quizman).ThenInclude(x => x.User)
                .Where(p => p.QuizmanId == quizmanId);

            if (dateFrom.HasValue)
                query = query.Where(p => p.Game.GameStartTime >= dateFrom);

            if (dateTo.HasValue)
                query = query.Where(p => p.Game.GameStartTime <= dateTo);

            return await query
                .OrderByDescending(p => p.Game.GameStartTime)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> CountByQuizmanAsync(
            Guid quizmanId,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            CancellationToken cancellationToken = default)
        {
            var query = _context.PointsHistory
                .Where(p => p.QuizmanId == quizmanId);

            if (dateFrom.HasValue)
                query = query.Where(p => p.Game.GameStartTime >= dateFrom);

            if (dateTo.HasValue)
                query = query.Where(p => p.Game.GameStartTime <= dateTo);

            return await query.CountAsync(cancellationToken);
        }
    }
}

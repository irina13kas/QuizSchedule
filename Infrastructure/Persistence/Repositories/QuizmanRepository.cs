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
    public class QuizmanRepository : Repository<Quizman>, IQuizmanRepository
    {
        public QuizmanRepository(AppDbContext context): base(context) { }

        public async Task<Quizman?> GetByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            return await _context.Quizmen
                .Include(q => q.User)
                .FirstOrDefaultAsync(q => q.UserId == userId
                && !q.User.IsDeleted,
                cancellationToken);
        }

        public async Task<List<Quizman>> GetFilteredAsync(
            string? searchQuery = null,
            bool? isActive = true,
            int skip = 0,
            int take = 10,
            CancellationToken cancellationToken = default)
        {
            var query = BuildFilteredQuery(searchQuery, isActive);
            return await query
                .Include(q => q.User)
                .Skip(skip)
                .Take(take)
                .OrderBy(q => q.User.Name)
                .ToListAsync(cancellationToken);
        }

        public Quizman Restore(Quizman quizman)
        {
            if (quizman == null)
                throw new ArgumentNullException(nameof(quizman));

            quizman.User.RestoreProfile();
            return quizman;
        }

        private IQueryable<Quizman> BuildFilteredQuery(string? searchQuery, bool? isActive)
        {
            var query = _context.Quizmen.AsQueryable();

            if (isActive.HasValue)
                query = query.Where(q => q.User.IsDeleted == !isActive.Value);

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                var normalized = searchQuery.Trim().ToLower();

                query = query.Where(q =>
                    q.User.Name.ToLower().Contains(normalized) ||
                    q.User.Login.ToLower().Contains(normalized));
            }

            return query;

        }
    }
}

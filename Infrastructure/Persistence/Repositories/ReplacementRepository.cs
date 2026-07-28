using Application.Common.Interfaces;
using Application.DTOs.Replacements;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.Persistence.Repositories
{
    public class ReplacementRepository : Repository<Replacement>, IReplacementRepository
    {
        public ReplacementRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<List<Replacement>> GetFilteredReplacementsByIdAsync(
            Guid quizmanId,
            DateTime? dateFrom,
            DateTime? dateTo,
            int skip = 0,
            int take = 20,
            CancellationToken cancellation = default)
        {
            var query = _context.Replacements
                .Include(r => r.Quizeman).ThenInclude(r => r.User)
                .Include(r => r.TakenAdmin).ThenInclude(r => r.User)
                .Where(r => r.QuizemanId == quizmanId);

            var dateFromOnly = dateFrom.HasValue ? DateOnly.FromDateTime(dateFrom.Value) : DateOnly.MinValue;
            if (dateFrom.HasValue)
                query = query.Where(r => r.Date >= dateFromOnly);

            var dateToOnly = dateTo.HasValue ? DateOnly.FromDateTime(dateTo.Value) : DateOnly.MinValue;
            if (dateTo.HasValue)
                query = query.Where(r => r.Date <= dateToOnly);

            return await query
                .Skip(skip)
                .Take(take)
                .OrderBy(r => r.Date)
                .ToListAsync(cancellation);
        }

        public async Task<List<Replacement>> GetFilteredReplacementsAsync(
            DateTime? dateFrom,
            DateTime? dateTo,
            int skip = 0,
            int take = 20,
            CancellationToken cancellation = default)
        {
            var query = _context.Replacements
                .Include(r => r.Quizeman).ThenInclude(r => r.User)
                .Include(r => r.TakenAdmin).ThenInclude(r => r.User)
                .Where(r => r.Date >= DateOnly.FromDateTime(dateFrom.Value) 
                && r.Date <= DateOnly.FromDateTime(dateTo.Value)
                && r.Status == ReplacementStatus.Wait);

            return await query
                .Skip(skip)
                .Take(take)
                .OrderBy(r => r.Date)
                .ToListAsync(cancellation);
        }

        public async Task<int> CountReplacementsAsync(
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Replacements.Where(r => r.Status == ReplacementStatus.Wait);
            var dateFromOnly = dateFrom.HasValue ? DateOnly.FromDateTime(dateFrom.Value) : DateOnly.MinValue;
            if (dateFrom.HasValue)
                query = query.Where(r => r.Date >= dateFromOnly);

            var dateToOnly = dateTo.HasValue ? DateOnly.FromDateTime(dateTo.Value) : DateOnly.MinValue;
            if (dateTo.HasValue)
                query = query.Where(r => r.Date <= dateToOnly);

            return await query
                .CountAsync();
        }

        public async Task<int> CountByQuizmanAsync(
            Guid quizmanId,
            DateTime? dateTo = null,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Replacements.Where(r => r.QuizemanId == quizmanId);
            query = query.Where(r => r.Date >= DateOnly.FromDateTime(DateTime.Today));

            var dateToOnly = dateTo.HasValue ? DateOnly.FromDateTime(dateTo.Value) : DateOnly.MinValue;
            if (dateTo.HasValue)
                query = query.Where(r => r.Date <= dateToOnly);

            return await query
                .CountAsync();
        }

        public async Task<Replacement?> GetReplacementByDateAndQuizmanIdAsync(
            Guid quizmanId,
            DateTime? date,
            CancellationToken cancellationToken)
        {
            return await _context.Replacements
                .Include(r => r.Quizeman)
                .FirstOrDefaultAsync(r => r.QuizemanId == quizmanId
                && r.Date == DateOnly.FromDateTime(date.Value)
                ,cancellationToken);
        }
    }
}

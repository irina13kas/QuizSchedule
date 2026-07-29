using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Persistence.Repositories
{
    public class SmartRepository : Repository<Smart>, ISmartRepository
    {
        public SmartRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<List<Smart>> GetAllAvailableQuizmenAsync(
            DateTime date,
            CancellationToken cancellationToken = default)
        {
            return await _context.Smarts
                .Include(s => s.Quizman).ThenInclude(u => u.User)
                .Where(s => s.Date == DateOnly.FromDateTime(date) 
                    && s.Status == SmartStatus.Available)
                .OrderBy(s => s.Quizman.User.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Smart>> GetSmartDatesForQuizmanByStatusAsync(
            Guid quizemanId,
            SmartStatus? status,
            DateTime? dateFrom,
            DateTime? dateTo,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Smarts
                 .Where(s => s.QuizmanId == quizemanId);

            if (dateFrom.HasValue)
                query = query.Where(s => s.Date >= DateOnly.FromDateTime(dateFrom.Value));
            else
                query = query.Where(s => s.Date >= DateOnly.FromDateTime(DateTime.Now.Date));

            if (dateTo.HasValue)
                query = query.Where(s => s.Date >= DateOnly.FromDateTime(dateTo.Value));
            else
                query = query.Where(s => s.Date >= DateOnly.FromDateTime(DateTime.Now.Date.AddDays(10)));

            if (status.HasValue)
                query = query.Where(s => s.Status == status);

            return await query
                .OrderBy(s => s.Date)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Smart>> GetSmartDatesForQuizmanAsync(
            Guid quizemanId,
            DateTime? dateFrom,
            DateTime? dateTo,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Smarts
                 .Where(s => s.QuizmanId == quizemanId);

            if (dateFrom.HasValue)
                query = query.Where(s => s.Date >= DateOnly.FromDateTime(dateFrom.Value));
            else
                query = query.Where(s => s.Date >= DateOnly.FromDateTime(DateTime.Now.Date));

            if (dateTo.HasValue)
                query = query.Where(s => s.Date >= DateOnly.FromDateTime(dateTo.Value));
            else
                query = query.Where(s => s.Date >= DateOnly.FromDateTime(DateTime.Now.Date.AddDays(10)));

            return await query
                .OrderBy(s => s.Date)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsQuizemanAvailableOnThisDate(
            Guid quizemanId,
            DateTime date,
            CancellationToken cancellationToken = default)
        {
            return await _context.Smarts
                .AnyAsync(s => s.QuizmanId == quizemanId 
                && s.Date == DateOnly.FromDateTime(date)
                && s.Status == SmartStatus.Available, cancellationToken);

        }

        public async Task<Smart?> GetByIdAndDateAsync(
            Guid quizemanId,
            DateTime date,
            CancellationToken cancellationToken = default)
        {
            return await _context.Smarts
                .FirstOrDefaultAsync(s => s.QuizmanId == quizemanId
                && s.Date == DateOnly.FromDateTime(date), cancellationToken);

        }
    }
}

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

        public async Task<List<DateOnly>> GetAvailableDatesForQuizmenAsync(
            Guid quizemanId,
            DateTime dateFrom,
            DateTime dateTo,
            CancellationToken cancellationToken = default)
        {
            return await _context.Smarts
                .Where(s => s.QuizmanId == quizemanId 
                && s.Date >= DateOnly.FromDateTime(dateFrom)
                && s.Date <= DateOnly.FromDateTime(dateTo)
                && s.Status == SmartStatus.Available)
                .Select(s => s.Date)
                .OrderBy(s => s.Day)
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

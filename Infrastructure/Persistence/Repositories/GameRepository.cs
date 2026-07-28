using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        public GameRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Game?> GetByNameAsync(
            string name, 
            CancellationToken cancellationToken)
        {
            return await _context.Games
                .FirstOrDefaultAsync(g => g.Name.ToLower() == name.ToLower(), cancellationToken);
        }

        public async Task<Game?> GetByIdWithParticipantsAsync(
            Guid gameId, 
            CancellationToken cancellationToken)
        {
            return await _context.Games
                .Include(g => g.Participants)
                .FirstOrDefaultAsync(g => gameId == g.Id, cancellationToken);

        }

        public async Task<List<Game>> GetByDateWithParticipantsAsync(
            DateTime date, 
            CancellationToken cancellationToken)
        {
            return await _context.Games
                .Include(g => g.Participants)
                .Where(g => g.GameStartTime == date)
                .OrderBy(g => g.GameStartTime).ThenBy(g => g.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Game>> GetByDatePeriodWithParticipantsAsync(
            DateTime dateFrom,
            DateTime dateTo,
            CancellationToken cancellationToken)
        {
            return await _context.Games
                .Include(g => g.Participants)
                .Where(g => g.GameStartTime >= dateFrom && g.GameStartTime <= dateTo)
                .OrderBy(g => g.GameStartTime).ThenBy(g => g.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsBarBuzyAsync(
            Guid barId,
            DateTime gameStartTime,
            CancellationToken cancellationToken = default)
        {
            return await _context.Games
                .AnyAsync(g => g.GameStartTime == gameStartTime 
                && g.BarId == barId
                && (g.Status == GameStatus.Available
                || g.Status == GameStatus.Draft),
                cancellationToken);
        }

        public async Task<bool> IsMasterBuzyAsync(
            Guid masterId,
            DateTime gameStartTime,
            CancellationToken cancellationToken = default)
        {
            return await _context.Games
                .AnyAsync(g => g.GameStartTime == gameStartTime 
                && g.MasterId == masterId
                && (g.Status == GameStatus.Available
                || g.Status == GameStatus.Draft),
                cancellationToken);
        }

        public async Task<bool> IsPhotographerBuzyAsync(
            Guid photographerId,
            DateTime gameStartTime,
            CancellationToken cancellationToken = default)
        {
            return await _context.Games
                .AnyAsync(g => g.GameStartTime == gameStartTime 
                && g.PhotographerId == photographerId
                && (g.Status == GameStatus.Available
                || g.Status == GameStatus.Draft),
                cancellationToken);
        }

        public async Task<bool> IsDjBuzyAsync(
            Guid djId,
            DateTime gameStartTime,
            CancellationToken cancellationToken = default)
        {
            return await _context.Games
                .AnyAsync(g => g.GameStartTime == gameStartTime 
                && g.DjId == djId
                && (g.Status == GameStatus.Available
                || g.Status == GameStatus.Draft),
                cancellationToken);
        }

        public async Task<bool> IsAdminBuzyAsync(
            Guid adminId,
            DateTime gameStartTime,
            CancellationToken cancellationToken = default)
        {
            return await _context.Games
                .AnyAsync(g => g.GameStartTime == gameStartTime 
                && g.ResponsibleAdminId == adminId
                && (g.Status == GameStatus.Available
                || g.Status == GameStatus.Draft),
                cancellationToken);
        }
    }
}

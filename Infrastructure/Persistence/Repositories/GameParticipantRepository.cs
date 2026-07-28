using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class GameParticipantRepository : Repository<GameParticipant>, IGameParticipantRepository
    {
        public GameParticipantRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<GameParticipant>> GetParticipantsByGameIdAsync(
            Guid gameId,
            CancellationToken cancellationToken = default)
        {
            return await _context.Participants
                .Include(p => p.Quizman).ThenInclude(p => p.User)
                .Where(p => p.GameId == gameId)
                .OrderByDescending(p => p.Game.GameStartTime)
                .ToListAsync(cancellationToken);
        }

        public Task<bool> AlreadyParticipantOnGameAsync(
            Guid quizemanId,
            DateTime gameStartTime,
            CancellationToken cancellationToken = default)
        {
            return _context.Participants
                .Include(p => p.Game)
                .AnyAsync(p => p.Game.GameStartTime == gameStartTime
                && p.QuizmanId == quizemanId
                && p.Game.Status != GameStatus.Cancelled,
                cancellationToken);

        }

        public void DeleteAllParticipantsFromGame(
            Guid gameId,
            CancellationToken cancellationToken = default)
        {
            var allParticipants = _context.Participants
                .Where(p => p.GameId == gameId);

            _context.Participants
                .RemoveRange(allParticipants);
        }

        public async Task<bool> IsQuizmanTakesPlaceOnGame(
            Guid quizmanId,
            Guid gameId,
            CancellationToken cancellationToken = default)
        {
            return await _context.Participants
                .AnyAsync(p => p.GameId == gameId 
                && p.QuizmanId == quizmanId 
                );
        }
    }
}

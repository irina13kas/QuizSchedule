using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IGameParticipantRepository : IRepository<GameParticipant>
    {
        Task<List<GameParticipant>> GetParticipantsByGameIdAsync(
            Guid gameId, 
            CancellationToken cancellationToken = default);

        Task<bool> AlreadyParticipantOnGameAsync(
            Guid quizemanId,
            DateTime gameStartTime,
            CancellationToken cancellationToken = default);

        void DeleteAllParticipantsFromGame(
            Guid gameId, 
            CancellationToken cancellationToken = default);

        Task<bool> IsQuizmanTakesPlaceOnGame(
            Guid quizmanId,
            Guid gameId,
            CancellationToken cancellationToken = default);

    }
}

using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        Task<Game?> GetByNameAsync(string name, CancellationToken cancellationToken);

        Task<Game?> GetByIdWithParticipantsAsync(Guid gameId, CancellationToken cancellationToken);

        Task<List<Game>> GetByDateWithParticipantsAsync(DateTime date, CancellationToken cancellationToken);

        Task<List<Game>> GetByDatePeriodWithParticipantsAsync(DateTime dateFrom, 
            DateTime dateTo, 
            CancellationToken cancellationToken);

        //Task<int> CountFilteredAsync(
        //string? searchQuery = null,
        //string? status = null,
        //DateTime? fromDate = null,
        //DateTime? toDate = null,
        //Guid? barId = null,
        //Guid? adminId = null,
        //CancellationToken cancellationToken = default);

        //Task<List<Game>> GetScheduleAsync(
        //DateTime fromDate,
        //DateTime toDate,
        //string? status = null,
        //Guid? barId = null,
        //Guid? adminId = null,
        //string? searchQuery = null,
        //string? sortBy = null,
        //Guid? quizemanParticipantId = null,
        //CancellationToken cancellationToken = default);

        //Task<bool> HasQuizemanConflictAsync(
        //Guid quizemanId,
        //DateTime gameDate,
        //Guid? excludeGameId = null,
        //CancellationToken cancellationToken = default);

        //Task<bool> IsQuizemanParticipantAsync(
        //Guid gameId,
        //Guid quizemanId,
        //CancellationToken cancellationToken = default);

        //Task<bool> AddParticipantToGameAsync(
        //Guid gameId,
        //Guid panticipantId,
        //CancellationToken token = default);

        //bool RemoveParticipantFromGame(
        //Guid gameId,
        //Guid participantId,
        //CancellationToken cancellationToken);

        Task<bool> IsBarBuzyAsync(
            Guid barId,
            DateTime gameStartTime,
            CancellationToken cancellationToken= default);

        Task<bool> IsMasterBuzyAsync(
            Guid masterId,
            DateTime gameStartTime,
            CancellationToken cancellationToken = default);

        Task<bool> IsPhotographerBuzyAsync(
            Guid photographerId,
            DateTime gameStartTime,
            CancellationToken cancellationToken = default);

        Task<bool> IsDjBuzyAsync(
            Guid djId,
            DateTime gameStartTime,
            CancellationToken cancellationToken = default);

        Task<bool> IsAdminBuzyAsync(
            Guid adminId,
            DateTime gameStartTime,
            CancellationToken cancellationToken = default);
    }
}

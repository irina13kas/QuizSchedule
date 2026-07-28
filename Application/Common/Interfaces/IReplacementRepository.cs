using Application.DTOs.Replacements;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IReplacementRepository : IRepository<Replacement>
    {
        Task<List<Replacement>> GetFilteredReplacementsByIdAsync(Guid quizmanId,
            DateTime? dateFrom,
            DateTime? dateTo,
            int skip = 0,
            int take = 20,
            CancellationToken cancellation = default);

        Task<List<Replacement>> GetFilteredReplacementsAsync(
            DateTime? dateFrom,
            DateTime? dateTo,
            int skip = 0,
            int take = 20,
            CancellationToken cancellation = default);

        Task<int> CountReplacementsAsync(
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            CancellationToken cancellationToken = default);

        Task<int> CountByQuizmanAsync(
            Guid quizmanId,
            DateTime? dateTo = null,
            CancellationToken cancellationToken = default);

        Task<Replacement?> GetReplacementByDateAndQuizmanIdAsync(
            Guid quizmanId,
            DateTime? date,
            CancellationToken cancellationToken); 
    }
}

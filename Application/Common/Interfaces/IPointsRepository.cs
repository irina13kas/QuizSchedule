using Application.DTOs.Points;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IPointsRepository : IRepository<Domain.Entities.Point>
    {
        Task<List<Domain.Entities.Point>> GetFilteredPointsHistoryAsync(
            Guid quizmanId,
            DateTime? dateFrom = null, 
            DateTime? dateTo = null,
            int skip = 0,
            int take = 20,
            CancellationToken cancellationToken = default);

        Task<int> CountByQuizmanAsync(
            Guid quizmanId,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            CancellationToken cancellationToken = default);
    }
}

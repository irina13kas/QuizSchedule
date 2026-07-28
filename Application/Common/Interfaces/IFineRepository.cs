using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IFineRepository : IRepository<Fine>
    {
        Task<List<Fine>> GetFineHistoryAsync(
            Guid quizemanId,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int skip = 0,
            int take = 20,
            CancellationToken cancellationToken = default
            );

        Task<int> CountByQuizemanAsync(
            Guid quizemanId,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            CancellationToken cancellationToken = default);
    }
}

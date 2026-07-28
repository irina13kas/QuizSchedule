using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IPushRepository : IRepository<PushToken>
    {
        Task<PushToken?> GetTokenAsync(
            string token,
            CancellationToken cancellationToken = default);

        Task<List<PushToken>> GetAllActiveTokensAsync(
            Guid userId,
            CancellationToken cancellationToken = default);

        Task DeactivateAllByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default);
    }
}

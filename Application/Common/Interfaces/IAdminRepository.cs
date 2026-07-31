using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IAdminRepository : IRepository<Admin>
    {
        Task<Admin> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

        Task<Admin?> GetByUserIdIgnoreFiltersAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}

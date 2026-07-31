using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class AdminRepository : Repository<Admin>, IAdminRepository
    {
        public AdminRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Admin?> GetByUserIdAsync(
            Guid userId, 
            CancellationToken cancellationToken = default)
        {
            return await _context.Admins
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.UserId == userId, 
                cancellationToken);
        }

        public async Task<Admin?> GetByUserIdIgnoreFiltersAsync(
            Guid userId, 
            CancellationToken cancellationToken = default)
        {
            return await _context.Admins
               .Include(x => x.User)
               .IgnoreQueryFilters()
               .FirstOrDefaultAsync(x => x.UserId == userId,
               cancellationToken);
        }
    }
}

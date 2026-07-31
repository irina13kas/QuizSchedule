using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IQuizmanRepository : IRepository<Quizman>
    {
        Task<Quizman?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);

        Task<List<Quizman>> GetFilteredAsync(
            string? searchQuery = null,
            bool? isActive = true,
            int skip = 0,
            int take = 10,
            CancellationToken cancellationToken = default);

        Quizman Restore(Quizman quizman);

        Task<Quizman?> GetByUserIdIgnoreFiltersAsync(
            Guid userId, 
            CancellationToken cancellationToken);
    }
}

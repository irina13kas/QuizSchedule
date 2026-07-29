using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ISmartRepository : IRepository<Smart>
    {
        Task<List<Smart>> GetAllAvailableQuizmenAsync(
            DateTime date, 
            CancellationToken cancellationToken = default);
        Task<List<Smart>> GetSmartDatesForQuizmanByStatusAsync(
            Guid quizemanId,
            SmartStatus? status,
            DateTime? dateFrom, 
            DateTime? dateTo,
            CancellationToken cancellationToken = default);

        Task<List<Smart>> GetSmartDatesForQuizmanAsync(
            Guid quizemanId,
            DateTime? dateFrom,
            DateTime? dateTo,
            CancellationToken cancellationToken = default);

        Task<bool> IsQuizemanAvailableOnThisDate(
            Guid quizemanId,
            DateTime date,
            CancellationToken cancellationToken = default);

        Task<Smart?> GetByIdAndDateAsync(
            Guid quizemanId,
            DateTime date,
            CancellationToken cancellationToken = default);
    }
}

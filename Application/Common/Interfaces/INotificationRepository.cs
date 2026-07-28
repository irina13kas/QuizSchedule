using Application.DTOs.Notifications;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<List<Notification>> GetAllNotificationsByUserIdAsync (
            Guid userId, 
            DateTime? fromDate,
            DateTime? toDate,
            bool? isRead = null,
            string? type = null,
            int skip = 0,
            int take = 20,
            CancellationToken cancellationToken = default
            );

        Task<List<Notification>> GetAllNotificationsByUserIdWithoutPaginationAsync(
           Guid userId,
           CancellationToken cancellationToken = default
           );

        Task<List<Notification>> GetAllNotificationsByUserIdWithUnreadStatusAsync(
           Guid userId,
           CancellationToken cancellationToken = default
           );

        Task<int> CountNotificationsAsync(
            Guid userId,
            DateTime? fromDate,
            DateTime? toDate,
            bool? isRead,
            CancellationToken cancellationToken);
    }
}

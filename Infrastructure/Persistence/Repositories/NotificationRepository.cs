using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        public NotificationRepository(AppDbContext context): base(context){ }

        public async Task<List<Notification>> GetAllNotificationsByUserIdAsync(
            Guid userId,
            DateTime? fromDate,
            DateTime? toDate,
            bool? isRead = null,
            string? type = null,
            int skip = 0,
            int take = 20,
            CancellationToken cancellationToken = default)
        {
            var query = BuildFilteredQuery(userId, isRead, type, fromDate, toDate);
            return await query
                .Skip(skip)
                .Take(take)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Notification>> GetAllNotificationsByUserIdWithoutPaginationAsync(
           Guid userId,
           CancellationToken cancellationToken = default)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Notification>> GetAllNotificationsByUserIdWithUnreadStatusAsync(
           Guid userId,
           CancellationToken cancellationToken = default)
        {
            return await _context.Notifications
                .Where (n => n.UserId == userId && !n.IsRead)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> CountNotificationsAsync(
            Guid userId,
            DateTime? fromDate,
            DateTime? toDate,
            bool? isRead,
            CancellationToken cancellationToken)
        {
            var query = _context.Notifications
                .Where(n => n.UserId == userId);

            if (fromDate.HasValue)
                query = query.Where(n => n.CreatedAt >= fromDate);

            if (toDate.HasValue)
                query = query.Where(n => n.CreatedAt <= toDate);

            if(isRead.HasValue)
                query = query.Where(n => n.IsRead == isRead);

            return await query.CountAsync(cancellationToken);
        }

        private IQueryable<Notification> BuildFilteredQuery(
            Guid userId,
            bool? isRead,
            string? type,
            DateTime? dateFrom,
            DateTime? dateTo)
        {
            var query = _context.Notifications
                .Where(n => n.UserId == userId);

            if(isRead.HasValue)
                query = query.Where(n => n.IsRead == isRead);

            if (!string.IsNullOrEmpty(type)
                && Enum.TryParse<NotificationType>(type, true, out var notificationType))
                query = query.Where(p => p.Type == notificationType);

            if (dateFrom.HasValue)
                query = query.Where(n => n.CreatedAt >= dateFrom.Value);

            if (dateTo.HasValue)
                query = query.Where(n => n.CreatedAt <= dateTo.Value);

            return query;
        }
    }
}

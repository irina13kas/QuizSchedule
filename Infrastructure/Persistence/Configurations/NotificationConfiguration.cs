using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("notifications");
            builder.HasKey(x => x.Id);

            builder.Property(n => n.Message)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(n => n.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(n => n.Type)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(30);

            builder.Property(n => n.IsRead)
                .HasDefaultValue(false);

            builder.Property(b => b.CreatedAt)
                .IsRequired();

            builder.HasIndex(n => n.CreatedAt);
            builder.HasIndex(n => new
            {
                n.UserId,
                n.IsRead
            });
            builder.HasIndex(n => n.Type);
            builder.HasIndex(n => n.UserId);

            builder.HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

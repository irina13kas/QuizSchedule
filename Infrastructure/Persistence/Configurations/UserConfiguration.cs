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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(t => t.Id);

            builder.Property(u => u.Login)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(u => u.PhotoUrl)
                .HasMaxLength(200);

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.VkUrl)
                .IsRequired();

            builder.Property(u => u.IsFirstEnterFinished)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(u => u.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(u => u.Role)
                .HasConversion<string>()
                .HasDefaultValue(UserRole.Quizman)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(u => u.Login).IsUnique();

            builder.HasIndex(u => u.VkUrl)
                .IsUnique()
                .HasFilter("[VkUrl] IS NOT NULL");

            builder.HasQueryFilter(u => !u.IsDeleted);
        }
    }
}

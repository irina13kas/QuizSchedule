using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class RefreshTokenConfiquration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("refresh_tokens");
            builder.HasKey(rt => rt.Id);

            builder.Property(rt => rt.Token)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(rt => rt.ExpiresAt)
                .IsRequired();

            builder.Property(q => q.CreatedAt)
                .IsRequired();

            builder.Ignore(x => x.IsActive);
            builder.Ignore(x => x.IsExpired);
            builder.Ignore(x => x.IsRevoked);

            builder.HasIndex(rt => rt.Token)
                .IsUnique();

            builder.HasIndex(rt => rt.ExpiresAt);
            builder.HasIndex(rt => rt.UserId);
            //активные токены пользователя
            builder.HasIndex(rt => new
            {
                rt.UserId,
                rt.RevokedAt
            });

            builder.HasOne(rt => rt.User)
                .WithMany()
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

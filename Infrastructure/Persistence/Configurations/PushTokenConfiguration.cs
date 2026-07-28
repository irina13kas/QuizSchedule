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
    public class PushTokenConfiguration : IEntityTypeConfiguration<PushToken>
    {
        public void Configure(EntityTypeBuilder<PushToken> builder)
        {
            builder.ToTable("push_tokens");

            builder.HasKey(x => x.Id);

            builder.Property(pt => pt.IsActive)
                .HasDefaultValue(true);

            builder.Property(pt => pt.Token)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(pt => pt.Platform)
                .HasConversion<string>()
                .HasDefaultValue(PlatformType.Android)
                .IsRequired();

            builder.Property(q => q.CreatedAt)
                .IsRequired();

            builder.HasIndex(pt => pt.Token)
                .IsUnique();

            builder.HasIndex(pt => new
            {
                pt.UserId,
                pt.IsActive
            });

            builder.HasOne(pt => pt.User)
                .WithMany()
                .HasForeignKey(pt => pt.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

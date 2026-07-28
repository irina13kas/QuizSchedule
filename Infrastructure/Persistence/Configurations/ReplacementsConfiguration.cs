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
    public class ReplacementsConfiguration : IEntityTypeConfiguration<Replacement>
    {
        public void Configure(EntityTypeBuilder<Replacement> builder)
        {
            builder.ToTable("replacements");
            builder.HasKey(x => x.Id);

            builder.Property(r => r.Date)
                .IsRequired();

            builder.Property(r => r.Comment)
                .HasMaxLength(200);

            builder.Property(r => r.Status)
                .HasConversion<string>()
                .HasDefaultValue(ReplacementStatus.Wait);

            builder.Property(r => r.CreatedAt)
                .IsRequired();

            builder.Property(r => r.IsFullShift)
                .IsRequired();

            builder.HasIndex(r => r.Date);
            builder.HasIndex(r => r.QuizemanId);
            builder.HasIndex(r => r.TakenAdminId);
            builder.HasIndex(r => new
            {
                r.QuizemanId,
                r.Date,
            });

            builder.HasOne(r => r.Quizeman)
                .WithMany()
                .HasForeignKey(r => r.QuizemanId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.TakenAdmin)
                .WithMany()
                .HasForeignKey(r => r.TakenAdminId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable(t => t.HasCheckConstraint("CK_Date_Later_Now",
                @"""Date"" >= CURRENT_DATE"));
        }
    }
}

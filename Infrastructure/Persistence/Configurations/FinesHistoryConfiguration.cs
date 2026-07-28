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
    public class FinesHistoryConfiguration : IEntityTypeConfiguration<Fine>
    {
        public void Configure(EntityTypeBuilder<Fine> builder)
        {
            builder.ToTable("fines_history");

            builder.HasKey(x => x.Id);

            builder.Property(f => f.Amount)
                .IsRequired();

            builder.Property(f => f.AdminId)
                .IsRequired();

            builder.Property(f => f.QuizmanId)
                .IsRequired();

            builder.Property(f => f.Comment)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(f => f.IsClosed)
                .HasDefaultValue(false);

            builder.Property(b => b.CreatedAt)
                .IsRequired();

            builder.HasIndex(x => x.QuizmanId);

            builder.HasOne(f => f.Quizman)
                .WithMany(q => q.FinesHistory)
                .HasForeignKey(f => f.QuizmanId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.Admin)
                .WithMany(a => a.GivenFines)
                .HasForeignKey(f => f.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.Game)
                .WithMany()
                .HasForeignKey(f => f.GameId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable(t => t.HasCheckConstraint("CK_Amout_Positive", @"""Amount"" > 0"));
                
        }
    }
}

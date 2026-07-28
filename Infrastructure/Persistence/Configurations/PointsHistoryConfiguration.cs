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
    public class PointsHistoryConfiguration : IEntityTypeConfiguration<Point>
    {
        public void Configure(EntityTypeBuilder<Point> builder)
        {
            builder.ToTable("points_history");
            builder.HasKey(p => p.Id);

            builder.Property(f => f.Amount)
                .IsRequired();

            builder.Property(f => f.AdminId)
                .IsRequired();

            builder.Property(f => f.QuizmanId)
                .IsRequired();

            builder.Property(f => f.Comment)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(b => b.CreatedAt)
                .IsRequired();

            builder.HasIndex(x => x.QuizmanId);

            builder.HasOne(p => p.Quizman)
                .WithMany(q => q.PointsHistory)
                .HasForeignKey(p => p.QuizmanId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Admin)
                .WithMany(a => a.GivenPoints)
                .HasForeignKey(p => p.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Game)
                .WithMany()
                .HasForeignKey(p => p.GameId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable(t => t.HasCheckConstraint("CK_Amout_Positive", @"""Amount"" > 0"));
        }
    }
}

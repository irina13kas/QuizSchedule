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
    public class QuizmanConfiguration : IEntityTypeConfiguration<Quizman>
    {
        public void Configure(EntityTypeBuilder<Quizman> builder)
        {
            builder.ToTable("quizmen");
            builder.HasKey(q => q.Id);
            builder.HasAlternateKey(q => q.UserId);

            builder.Property(q => q.Points)
                .HasDefaultValue(0m)
                .HasPrecision(10)
                .IsRequired();

            builder.Property(q => q.Fines)
                .HasDefaultValue(0)
                .HasPrecision(10)
                .IsRequired();

            builder.Property(q => q.WorkShift)
                .HasDefaultValue(3m)
                .HasPrecision(10)
                .IsRequired();

            builder.Property(q => q.CreatedAt)
                .IsRequired();

            builder.HasOne(q => q.User)
                .WithOne(u => u.Quizman)
                .HasForeignKey<Quizman>(q => q.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

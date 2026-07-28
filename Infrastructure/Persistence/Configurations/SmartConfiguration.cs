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
    public class SmartConfiguration : IEntityTypeConfiguration<Smart>
    {
        public void Configure(EntityTypeBuilder<Smart> builder)
        {
            builder.ToTable("smart");
            builder.HasKey(x => x.Id);

            builder.Property(s => s.Date)
                .IsRequired();

            builder.Property(s => s.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(s => s.Comment)
                .HasMaxLength(200);

            builder.Property(s => s.CreatedAt)
                .IsRequired();

            builder.HasIndex(s => new
            {
                s.QuizmanId,
                s.Date,
            }).IsUnique();

            builder.HasOne(s => s.Quizman)
                .WithMany(q => q.Smart)
                .HasForeignKey(s => s.QuizmanId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

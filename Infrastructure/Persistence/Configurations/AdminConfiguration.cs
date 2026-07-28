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
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.ToTable("admins");

            builder.HasKey(a => a.Id);
            builder.HasAlternateKey(a => a.UserId);

            builder.Property(a => a.DaysOff)
                .HasMaxLength(30);

            builder.Property(q => q.CreatedAt)
                .IsRequired();

            builder.HasOne(a => a.User)
                .WithOne(u => u.Admin)
                .HasForeignKey<Quizman>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}

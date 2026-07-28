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
    public class DjConfiguration : IEntityTypeConfiguration<Dj>
    {
        public void Configure(EntityTypeBuilder<Dj> builder)
        {
            builder.ToTable("djs");

            builder.HasKey(d => d.Id);
            builder.HasAlternateKey(d => d.Name);

            builder.Property(b => b.CreatedAt)
                .IsRequired();

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}

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
    public class BarConfiguration : IEntityTypeConfiguration<Bar>
    {
        public void Configure(EntityTypeBuilder<Bar> builder)
        {
            builder.ToTable("bars");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(b => b.CreatedAt)
                .IsRequired();

            builder.HasIndex(b => new
            {
                b.Name,
                b.Address
            }).IsUnique();

            builder.ToTable(t => t.HasCheckConstraint("CK_Bar_Capacity_Range", @"""Capacity"" >= 0 AND ""Capacity""<=1000"));
        }
    }
}

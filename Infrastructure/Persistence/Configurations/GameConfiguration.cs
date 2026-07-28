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
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("games");
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(g => g.Status)
                .HasConversion<string>()
                .HasDefaultValue(GameStatus.Draft)
                .HasMaxLength(30);

            builder.Property(g => g.Partner)
                .HasMaxLength(100);

            builder.Property(g => g.CreateByAdminId)
                .IsRequired();

            builder.Property(g => g.GameStartTime)
                .IsRequired();

            builder.Property(g => g.WorkStartTime)
                .IsRequired();


            builder.HasIndex(g => g.Name)
                .IsUnique();

            builder.HasIndex(g => g.GameStartTime);

            builder.HasIndex(g => g.Status);

            builder.HasIndex(g => g.BarId);
            builder.HasIndex(g => g.DjId);
            builder.HasIndex(g => g.MasterId);
            builder.HasIndex(g => g.ResponsibleAdminId);
            builder.HasIndex(g => g.PhotographerId);

            builder.HasOne(g => g.Admin)
                .WithMany()
                .HasForeignKey(g => g.ResponsibleAdminId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(g => g.Bar)
                .WithMany()
                .HasForeignKey(g => g.BarId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(g => g.Dj)
                .WithMany()
                .HasForeignKey(g => g.DjId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(g => g.Master)
                .WithMany()
                .HasForeignKey(g => g.MasterId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(g => g.Photographer)
                .WithMany()
                .HasForeignKey(g => g.PhotographerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(g => g.Participants)
                .WithOne(p => p.Game)
                .HasForeignKey(p => p.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(t => t.HasCheckConstraint("CK_StartGameTime_Later_Now",
                @"""GameStartTime"" >= CURRENT_TIMESTAMP"));

            builder.ToTable(t => t.HasCheckConstraint("CK_WorkGameTime_Later_Now",
                @"""WorkStartTime"" >= CURRENT_TIMESTAMP"));

        }
    }
}

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
    public class GameParticipantsConfiguration : IEntityTypeConfiguration<GameParticipant>
    {
        public void Configure(EntityTypeBuilder<GameParticipant> builder)
        {
            builder.ToTable("game_participants");

            builder.HasKey(gp => gp.Id);

            builder.Property(gp => gp.Role)
                .HasConversion<string>()
                .HasDefaultValue(ParticipantRole.None);

            builder.Property(b => b.CreatedAt)
                .IsRequired();

            builder.Property(gp => gp.QuizmanId)
                .IsRequired();

            builder.HasIndex(gp => new
            {
                gp.GameId,
                gp.QuizmanId,
            }).IsUnique();

            builder.HasIndex(gp => gp.QuizmanId);
            builder.HasIndex(gp => gp.GameId);

            builder.HasOne(gp => gp.Quizman)
                .WithMany(q => q.Games)
                .HasForeignKey(gp => gp.QuizmanId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

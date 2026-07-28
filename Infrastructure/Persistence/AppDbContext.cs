using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    //Связь Domain и сущности таблиц
    //Отслеживание изменений
    //Управление транзакциями
    //Генерация SQL
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Quizman> Quizmen { get; set; }
        public DbSet<Admin> Admins { get; set; }

        public DbSet<Game> Games { get; set; }
        public DbSet<GameParticipant> Participants { get; set; }
        public DbSet<Replacement> Replacements { get; set; }
        public DbSet<Smart> Smarts { get; set; }

        public DbSet<Bar> Bars { get; set; }
        public DbSet<Dj> Djs { get; set; }
        public DbSet<Master> Masters { get; set; }
        public DbSet<Photographer> Photographers { get; set; }

        public DbSet<Fine> FinesHistory { get; set; }
        public DbSet<Point> PointsHistory { get; set; }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<PushToken> PushTokens { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            SetAuditFields();
            return base.SaveChanges();
        }

        private void SetAuditFields()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            foreach(var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.SetCreatedAt(DateTime.Now);
                        break;
                }
            }
        }
    }
}

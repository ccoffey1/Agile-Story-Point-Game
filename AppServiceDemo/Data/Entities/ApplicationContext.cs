using AppServiceDemo.Data.Entities.Base;
using AppServiceDemo.Data.Entities.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AppServiceDemo.Data.Entities
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<GameSession> GameSessions { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RemovePluralizingTableNameConvention();
        }

        public override int SaveChanges()
        {
            GenerateTimeStamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            GenerateTimeStamps();
            return (await base.SaveChangesAsync(true, cancellationToken));
        }

        private void GenerateTimeStamps()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is TimeStampedEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((TimeStampedEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((TimeStampedEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }
        }
    }
}

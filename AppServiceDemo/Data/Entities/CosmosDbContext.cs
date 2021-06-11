using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;

namespace AppServiceDemo.Data.Entities
{
    public class CosmosDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<GameSession> GameSessions { get; set; }

        public CosmosDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected CosmosDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOne(x => x.GameSession);
            modelBuilder.Entity<GameSession>().HasOne(x => x.Owner);

            // using reflection to dynamically create collections in cosmos for each dbset in this class
            var dbSets = typeof(CosmosDbContext).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType.IsGenericType && typeof(DbSet<>).IsAssignableFrom(p.PropertyType.GetGenericTypeDefinition()));

            foreach (var dbSet in dbSets)
            {
                var metadata = modelBuilder.Entity(dbSet.PropertyType.GetGenericArguments()[0])
                    .ToContainer(dbSet.Name);
            }
        }
    }
}

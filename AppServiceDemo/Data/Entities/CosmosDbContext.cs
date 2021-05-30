using Microsoft.EntityFrameworkCore;

namespace AppServiceDemo.Data.Entities
{
	public class CosmosDbContext : DbContext
	{
		public DbSet<Vote> Votes { get; set; }
		public DbSet<User> Users { get; set; }

		public CosmosDbContext(DbContextOptions options)
			: base(options)
		{ }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Vote>()
				.ToContainer(nameof(Votes))
				.HasKey(x => x.Id);

			modelBuilder.Entity<User>()
				.ToContainer(nameof(Users))
				.HasKey(x => x.Id);
		}
	}
}

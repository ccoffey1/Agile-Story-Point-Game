using Microsoft.EntityFrameworkCore;

namespace AppServiceDemo.Data.Models
{
	public class CosmosDbContext : DbContext
	{
		public DbSet<Vote> Votes { get; set; }

		public CosmosDbContext(DbContextOptions options)
			: base(options)
		{ }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Vote>()
				.ToContainer(nameof(Votes))
				.HasKey(x => x.Id);
		}
	}
}

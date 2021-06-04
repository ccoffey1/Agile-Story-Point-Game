using Microsoft.EntityFrameworkCore;

namespace AppServiceDemo.Data.Entities
{
	public class CosmosDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<GameSession> GameSessions { get; set; }

		public CosmosDbContext(DbContextOptions options)
			: base(options)
		{ }
	}
}

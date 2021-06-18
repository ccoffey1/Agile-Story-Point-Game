using Microsoft.EntityFrameworkCore;

namespace AppServiceDemo.Data.Entities
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<GameSession> GameSessions { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        { }
    }
}

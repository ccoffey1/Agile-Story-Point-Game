using AppServiceDemo.Data.Entities;
using AppServiceDemo.Data.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AppServiceDemo.Data.Repository
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Task<Player> GetByPlayerNameAsync(string playerName);
    }

    public class PlayerRepository : CosmosRepository<Player, CosmosDbContext>, IPlayerRepository
    {
        public PlayerRepository(
            CosmosDbContext context, 
            ILogger<PlayerRepository> logger) : base(context, logger)
        { }

        public async Task<Player> GetByPlayerNameAsync(string playerName)
        {
            return await _context.Players.FirstOrDefaultAsync(x => x.Name == playerName);
        }
    }
}

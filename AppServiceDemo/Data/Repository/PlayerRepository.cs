using AppServiceDemo.Data.Entities;
using AppServiceDemo.Data.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AppServiceDemo.Data.Repository
{
    public interface IPlayerRepository : IRepository<User>
    {
        Task<User> GetByPlayerNameAsync(string playerName);
    }

    public class PlayerRepository : CosmosRepository<User, CosmosDbContext>, IPlayerRepository
    {
        public PlayerRepository(
            CosmosDbContext context, 
            ILogger<PlayerRepository> logger) : base(context, logger)
        { }

        public async Task<User> GetByPlayerNameAsync(string playerName)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.PlayerName == playerName);
        }
    }
}

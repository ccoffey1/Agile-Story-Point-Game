using AppServiceDemo.Data.Entities;
using AppServiceDemo.Data.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AppServiceDemo.Data.Repository
{
    public interface IGameSessionRepository : IRepository<GameSession>
    {
        Task<GameSession> GetByJoinCodeAsync(string joinCode);
    }

    public class GameSessionRepository : BaseRepository<GameSession, ApplicationContext>, IGameSessionRepository
    {
        public GameSessionRepository(
            ApplicationContext context, 
            ILogger<GameSessionRepository> logger) : base(context, logger)
        { }

        public async Task<GameSession> GetByJoinCodeAsync(string joinCode)
        {
            return await _context.GameSessions
                .Include(x => x.Players)
                .FirstOrDefaultAsync(x => x.JoinCode == joinCode);
        }
    }
}

using AppServiceDemo.Data.Entities;
using AppServiceDemo.Data.Repository.Abstraction;
using Microsoft.Extensions.Logging;

namespace AppServiceDemo.Data.Repository
{
    public interface IGameSessionRepository : IRepository<GameSession>
    { }

    public class GameSessionRepository : BaseRepository<GameSession, ApplicationContext>, IGameSessionRepository
    {
        public GameSessionRepository(
            ApplicationContext context, 
            ILogger<GameSessionRepository> logger) : base(context, logger)
        { }
    }
}

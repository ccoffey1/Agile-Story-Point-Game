using AppServiceDemo.Data.Entities;
using AppServiceDemo.Data.Repository.Abstraction;

namespace AppServiceDemo.Data.Repository
{
    public interface IGameSessionRepository : IRepository<GameSession>
    { }

    public class GameSessionRepository : CosmosRepository<GameSession, CosmosDbContext>, IGameSessionRepository
    {
        public GameSessionRepository(CosmosDbContext context) : base(context)
        { }
    }
}

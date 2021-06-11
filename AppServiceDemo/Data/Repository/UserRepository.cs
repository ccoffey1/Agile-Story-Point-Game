using AppServiceDemo.Data.Entities;
using AppServiceDemo.Data.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AppServiceDemo.Data.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByPlayerNameAsync(string playerName);
    }

    public class UserRepository : CosmosRepository<User, CosmosDbContext>, IUserRepository
    {
        public UserRepository(CosmosDbContext context) : base(context)
        { }

        public async Task<User> GetByPlayerNameAsync(string playerName)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.PlayerName == playerName);
        }
    }
}

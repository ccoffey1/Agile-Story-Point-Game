using AppServiceDemo.Data.Entities;
using AppServiceDemo.Data.Repository.Abstraction;

namespace AppServiceDemo.Data.Repository
{
    public interface IUserRepository : IRepository<User>
    { }

    public class UserRepository : CosmosRepository<User, CosmosDbContext>, IUserRepository
    {
        public UserRepository(CosmosDbContext context) : base(context)
        { }
    }
}

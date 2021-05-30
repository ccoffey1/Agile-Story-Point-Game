using AppServiceDemo.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AppServiceDemo.Data.Repository
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User userEntity);
        Task<User> GetByIdAsync(Guid userId);
    }

    public class UserRepository : IUserRepository
    {
        private readonly CosmosDbContext _dbContext;

        public UserRepository(CosmosDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
        }

        public async Task<User> CreateAsync(User user)
        {
            var result = _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<User> GetByIdAsync(Guid userId)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == userId);
        }
    }
}

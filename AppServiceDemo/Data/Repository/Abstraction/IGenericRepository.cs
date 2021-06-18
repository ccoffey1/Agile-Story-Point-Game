using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppServiceDemo.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        Task ExecuteInTransaction(Func<Task> action);
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(int id);
    }
}

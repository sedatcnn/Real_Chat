using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Real_ChatApi.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task CreatAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task AddAsync(T entity);
        Task<T?> GetByFilterAsync(Expression<Func<T, bool>> filter);
    }
}

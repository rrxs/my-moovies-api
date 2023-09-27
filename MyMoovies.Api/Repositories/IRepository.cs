using MyMooviesApi.Repositories.Models;
using System.Linq.Expressions;

namespace MyMooviesApi.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        Task CreateAsync(T entity);
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter);
        Task<T> GetAsync(Guid id);
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task UpdateAsync(T entity);
        Task RemoveAsync(Guid id);
    }
}

using HotelListing.API.Core.Models;
using System.Linq.Expressions;

namespace HotelListing.API.Core.Repository.IRepository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<TResult> AddAsync<TSource, TResult>(TSource source);  // <---
        Task<TEntity?> GetAsync(int id);
        Task<TResult?> GetAsync<TResult>(int id);  // <---
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null);
        Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, bool>>? predicate = null);  // <---
        Task<PagedResult<TResult>> GetAllAsync<TResult>(PagingParameters queryParameters);
        public void Remove(TEntity entity);
        Task<bool> Exists(int id);
        Task UpdateAsync<TSource>(int id, TSource source); // <---
        Task SaveAsync();

    }
}

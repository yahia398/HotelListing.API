using HotelListing.API.Core.Models;
using System.Linq.Expressions;

namespace HotelListing.API.Core.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<T?> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null);
        Task<PagedResult<TResult>> GetAllAsync<TResult>(PagingParameters queryParameters);
        public void Remove(T entity);
        Task<bool> Exists(int id);
        Task SaveAsync();

    }
}

using System.Linq.Expressions;

namespace HotelListing.API.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<T?> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null);
        public void Remove(T entity);
        Task<bool> Exists(int id);
        Task SaveAsync();

    }
}

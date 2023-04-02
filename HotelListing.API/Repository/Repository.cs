using HotelListing.API.Data;
using HotelListing.API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HotelListing.API.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _db;
        private readonly DbSet<T> _dbSet;
        public Repository(AppDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate)
        {
            IQueryable<T> query = _dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var records = await query.ToListAsync();

            return records;
        }

        public async Task<T?> GetAsync(int id)
        {
            var record = await _dbSet.FindAsync(id);

            return record;
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveAsync()
        {
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException("Error saving changes to database", ex);
            }
        }

        public async Task<bool> Exists(int id)
        {
            var record = await GetAsync(id);
            return record != null;
        }

    }
}

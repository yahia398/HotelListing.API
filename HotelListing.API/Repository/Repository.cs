using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelListing.API.Data;
using HotelListing.API.Models;
using HotelListing.API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace HotelListing.API.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly DbSet<T> _dbSet;
        public Repository(AppDbContext db,IMapper mapper)
        {
            _db = db;
            _dbSet = _db.Set<T>();
            _mapper = mapper;
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

                throw new InvalidOperationException("Error during saving changes to the database", ex);
            }
        }

        public async Task<bool> Exists(int id)
        {
            var record = await GetAsync(id);
            return record != null;
        }

        public async Task<PagedResult<TResult>> GetAllAsync<TResult>(PagingParameters queryParameters)
        {
            var totalSize = await _dbSet.CountAsync();
            var items = await _dbSet.Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize)
                .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PagedResult<TResult>
            {
                TotalCount = totalSize,
                Items = items,
                PageNumber = queryParameters.PageNumber,
                RecordNumber = (queryParameters.PageNumber - 1) * queryParameters.PageSize

            };
        }
    }
}

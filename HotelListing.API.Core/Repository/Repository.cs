using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelListing.API.Core.Models;
using HotelListing.API.Core.Repository.IRepository;
using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace HotelListing.API.Core.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(AppDbContext db,IMapper mapper)
        {
            _db = db;
            _dbSet = _db.Set<TEntity>();
            _mapper = mapper;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate)
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var records = await query.ToListAsync();

            return records;
        }

        public async Task<TEntity?> GetAsync(int id)
        {
            var record = await _dbSet.FindAsync(id);

            return record;
        }

        public void Remove(TEntity entity)
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
                RecordNumber = (queryParameters.PageNumber) * queryParameters.PageSize

            };
        }

        public async Task<TResult> AddAsync<TSource, TResult>(TSource source)
        {
            var entity = _mapper.Map<TEntity>(source);
            await _dbSet.AddAsync(entity);
            return _mapper.Map<TResult>(entity);
        }

        public async Task<TResult?> GetAsync<TResult>(int id)
        {
            var record = await _dbSet.FindAsync(id);
            if (record == null)
            {
                return default;
            }
            return _mapper.Map<TResult>(record);
        }

        public async Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, bool>>? predicate = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.ProjectTo<TResult>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task UpdateAsync<TSource>(int id, TSource source)
        {
            var entity = await GetAsync(id);
            if(entity == null)
            {
                throw new NullReferenceException();
            }
            _mapper.Map(source, entity);

            _dbSet.Update(entity);
        }
    }
}

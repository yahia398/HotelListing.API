using HotelListing.API.Data;
using HotelListing.API.Models;
using HotelListing.API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Repository
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        private readonly AppDbContext _db;
        public CountryRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Country?> GetWithDetailsAsync(int id)
        {
            var country = await _db.Countries
                .Include(q => q.Hotels)
                .SingleOrDefaultAsync(c => c.Id == id);

            return country;
        }

        public void Update(Country entity)
        {
            _db.Countries.Update(entity);
        }
    }
}

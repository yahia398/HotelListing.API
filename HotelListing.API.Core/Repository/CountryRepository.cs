using AutoMapper;
using HotelListing.API.Data;
using HotelListing.API.Core.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Core.Models.ModelsDto.Country;
using AutoMapper.QueryableExtensions;

namespace HotelListing.API.Core.Repository
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mpper;

        public CountryRepository(AppDbContext db, IMapper mpper) : base(db, mpper)
        {
            _db = db;
            _mpper = mpper;
        }

        public async Task<CountryDto?> GetWithDetailsAsync(int id)
        {
            var country = await _db.Countries
                .Include(q => q.Hotels)
                .ProjectTo<CountryDto>(_mpper.ConfigurationProvider)
                .SingleOrDefaultAsync(c => c.Id == id);

            return country;
        }

        public void Update(Country entity)
        {
            _db.Countries.Update(entity);
        }
    }
}

using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelListing.API.Core.Repository.IRepository;
using HotelListing.API.Data;
using HotelListing.API.Models.ModelsDto.Hotel;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Core.Repository
{
    public class HotelRepository : Repository<Hotel>, IHotelRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mpper;

        public HotelRepository(AppDbContext db, IMapper mpper) : base(db, mpper)
        {
            _db = db;
            _mpper = mpper;
        }

        public async Task<HotelDto?> GetWithDetailsAsync(int id)
        {
            var hotel = await _db.Hotels
                .Include(x => x.Country)
                .ProjectTo<HotelDto>(_mpper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id);

            return hotel;
        }

        public void Update(Hotel entity)
        {
            _db.Hotels.Update(entity);
        }
    }
}

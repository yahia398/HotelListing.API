using HotelListing.API.Data;
using HotelListing.API.Models;
using HotelListing.API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Repository
{
    public class HotelRepository : Repository<Hotel>, IHotelRepository
    {
        private readonly AppDbContext _db;

        public HotelRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Hotel?> GetWithDetailsAsync(int id)
        {
            var hotel = await _db.Hotels
                .Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == id);

            return hotel;
        }

        public void Update(Hotel entity)
        {
            _db.Hotels.Update(entity);
        }
    }
}

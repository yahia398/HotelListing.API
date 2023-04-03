using HotelListing.API.Models;

namespace HotelListing.API.Repository.IRepository
{
    public interface IHotelRepository : IRepository<Hotel>
    {
        void Update(Hotel entity);
        Task<Hotel?> GetWithDetailsAsync(int id);
    }
}

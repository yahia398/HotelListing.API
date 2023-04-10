using HotelListing.API.Data;

namespace HotelListing.API.Core.Repository.IRepository
{
    public interface IHotelRepository : IRepository<Hotel>
    {
        void Update(Hotel entity);
        Task<Hotel?> GetWithDetailsAsync(int id);
    }
}

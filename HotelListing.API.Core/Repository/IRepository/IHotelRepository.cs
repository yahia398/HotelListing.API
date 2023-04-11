using HotelListing.API.Data;
using HotelListing.API.Models.ModelsDto.Hotel;

namespace HotelListing.API.Core.Repository.IRepository
{
    public interface IHotelRepository : IRepository<Hotel>
    {
        void Update(Hotel entity);
        Task<HotelDto?> GetWithDetailsAsync(int id);
    }
}

using HotelListing.API.Models;

namespace HotelListing.API.Repository.IRepository
{
    public interface ICountryRepository : IRepository<Country>
    {
        void Update(Country entity);
        Task<Country?> GetWithDetailsAsync(int id);
    }
}

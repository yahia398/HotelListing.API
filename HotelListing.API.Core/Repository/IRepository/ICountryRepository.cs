using HotelListing.API.Data;
using HotelListing.API.Models;

namespace HotelListing.API.Core.Repository.IRepository
{
    public interface ICountryRepository : IRepository<Country>
    {
        void Update(Country entity);
        Task<Country?> GetWithDetailsAsync(int id);
    }
}

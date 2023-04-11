using HotelListing.API.Core.Models.ModelsDto.Country;
using HotelListing.API.Data;
using HotelListing.API.Models;

namespace HotelListing.API.Core.Repository.IRepository
{
    public interface ICountryRepository : IRepository<Country>
    {
        void Update(Country entity);
        Task<CountryDto?> GetWithDetailsAsync(int id);
    }
}

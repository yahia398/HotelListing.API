using HotelListing.API.Core.Models.ModelsDto.Country;
using HotelListing.API.Core.Models.ModelsDto.Hotel;

namespace HotelListing.API.Models.ModelsDto.Hotel
{
    public class HotelDto : BaseHotelDto
    {
        public int Id { get; set; }

        public required GetCountriesDto Country { get; set; }
    }
}

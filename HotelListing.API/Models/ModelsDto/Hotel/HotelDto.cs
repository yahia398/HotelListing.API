using HotelListing.API.Models.ModelsDto.Country;

namespace HotelListing.API.Models.ModelsDto.Hotel
{
    public class HotelDto : BaseHotelDto
    {
        public int Id { get; set; }

        public required GetCountriesDto Country { get; set; }
    }
}

using HotelListing.API.Models.ModelsDto.Hotel;

namespace HotelListing.API.Models.ModelsDto.Country
{
    public class CountryDto : CountryBaseDto
    {
        public int Id { get; set; }

        public virtual IList<CountryHotelsDto>? Hotels { get; set; }
    }

}

using HotelListing.API.Core.Models.ModelsDto.Hotel;

namespace HotelListing.API.Core.Models.ModelsDto.Country
{
    public class CountryDto : CountryBaseDto
    {
        public int Id { get; set; }

        public virtual IList<CountryHotelsDto>? Hotels { get; set; }
    }

}

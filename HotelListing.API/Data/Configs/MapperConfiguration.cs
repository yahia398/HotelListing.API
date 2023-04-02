using AutoMapper;
using HotelListing.API.Models;
using HotelListing.API.Models.ModelsDto.Country;
using HotelListing.API.Models.ModelsDto.Hotel;

namespace HotelListing.API.Data.Configs
{
    public class MapperConfiguration : Profile
    {
        public MapperConfiguration()
        {
            CreateMap<Country, CreateCountryDto>().ReverseMap();
            CreateMap<Country, GetCountriesDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Country, UpdateCountryDto>().ReverseMap();

            CreateMap<Hotel, CountryHotelsDto>().ReverseMap();



        }
    }
}

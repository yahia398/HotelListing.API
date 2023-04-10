using AutoMapper;
using HotelListing.API.Models;
using HotelListing.API.Models.ModelsDto.ApiUser;
using HotelListing.API.Models.ModelsDto.Country;
using HotelListing.API.Models.ModelsDto.Hotel;

namespace HotelListing.API.Configurations
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
            CreateMap<Hotel, GetHotelsDto>().ReverseMap();
            CreateMap<Hotel, HotelDto>().ReverseMap();
            CreateMap<Hotel, CreateHotelDto>().ReverseMap();
            CreateMap<Hotel, UpdateHotelDto>().ReverseMap();

            CreateMap<ApiUser, ApiUserDto>().ReverseMap();
            CreateMap<ApiUser, LoginDto>().ReverseMap();

        }
    }
}

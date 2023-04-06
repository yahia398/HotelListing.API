using HotelListing.API.Models;

namespace HotelListing.API.Contracts
{
    public interface ITokenGenerator
    {
        Task<string> GenerateTokenAsync(ApiUser user);
    }
}

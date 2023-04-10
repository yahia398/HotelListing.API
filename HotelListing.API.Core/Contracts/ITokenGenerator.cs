using HotelListing.API.Data;

namespace HotelListing.API.Core.Contracts
{
    public interface ITokenGenerator
    {
        Task<string> GenerateTokenAsync(ApiUser user);
    }
}

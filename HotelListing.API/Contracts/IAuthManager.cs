using HotelListing.API.Models.ModelsDto.ApiUser;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> RegisterAsync(ApiUserDto apiUserDto);
        Task<bool> LoginAsync(LoginDto loginDto);
    }
}

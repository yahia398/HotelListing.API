using HotelListing.API.Models.ModelsDto.ApiUser;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> RegisterAsync(ApiUserDto apiUserDto);
        Task<IEnumerable<IdentityError>> RegisterAdminAsync(ApiUserDto apiUserDto);
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
        
    }
}

using HotelListing.API.Core.Models.ModelsDto.ApiUser;
using HotelListing.API.Data;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Core.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> RegisterAsync(ApiUserDto apiUserDto);
        Task<IEnumerable<IdentityError>> RegisterAdminAsync(ApiUserDto apiUserDto);
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
        Task<string> CreateRefreshToken(ApiUser user);
        Task<AuthResponseDto?> VerifyRefreshToken(AuthResponseDto request);


    }
}

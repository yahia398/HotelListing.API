using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Models;
using HotelListing.API.Models.ModelsDto.ApiUser;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;

        public AuthManager(IMapper mapper, UserManager<ApiUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<bool> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return false;
            }

            bool isValidUser = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!isValidUser)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<IdentityError>> RegisterAsync(ApiUserDto apiUserDto)
        {
            var user = _mapper.Map<ApiUser>(apiUserDto);

            var result = await _userManager.CreateAsync(user, apiUserDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            return result.Errors;
        }
    }
}

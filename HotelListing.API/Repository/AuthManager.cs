using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Models;
using HotelListing.API.Models.ModelsDto.ApiUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace HotelListing.API.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;
        private readonly ITokenGenerator _tokenGenerator;
        private ApiUser? _user;

        private const string LOGIN_PROVIDER = "HotelListngApi";
        private const string REFRESH_TOKEN = "RefreshToken";

        public AuthManager(IMapper mapper, 
            UserManager<ApiUser> userManager, 
            ITokenGenerator tokenGenerator)
        {
            _mapper = mapper;
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
        {
            var _user = await _userManager.FindByEmailAsync(loginDto.Email);
            if(_user == null)
                return null;

            bool isValidUser = await _userManager.CheckPasswordAsync(_user, loginDto.Password);

            if (!isValidUser)
                return null;

            var token = await _tokenGenerator.GenerateTokenAsync(_user);
            return new AuthResponseDto
            {
                Token = token,
                UserId = _user.Id
            };
        }

        public async Task<IEnumerable<IdentityError>> RegisterAsync(ApiUserDto apiUserDto)
        {
            var _user = _mapper.Map<ApiUser>(apiUserDto);

            var result = await _userManager.CreateAsync(_user, apiUserDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(_user, "User");
            }

            return result.Errors;
        }

        public async Task<IEnumerable<IdentityError>> RegisterAdminAsync(ApiUserDto apiUserDto)
        {
            var _user = _mapper.Map<ApiUser>(apiUserDto);

            var result = await _userManager.CreateAsync(_user, apiUserDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(_user, "Admin");
            }

            return result.Errors;
        }

        public async Task<string> CreateRefreshToken()
        {
            await _userManager.RemoveAuthenticationTokenAsync(_user,
                LOGIN_PROVIDER, REFRESH_TOKEN);

            var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user,
                LOGIN_PROVIDER, REFRESH_TOKEN);

            var result = await _userManager.SetAuthenticationTokenAsync(_user,
                LOGIN_PROVIDER, REFRESH_TOKEN, newRefreshToken);

            return newRefreshToken;
        }

        public async Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(request.Token);
            var username = tokenContent.Claims.ToList()
                .FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Email)?.Value;
            _user = await _userManager.FindByNameAsync(username);

            if(_user == null || _user.Id != request.UserId)
            {
                return null;
            }
            var isValidRefreshToken = await _userManager
                .VerifyUserTokenAsync(_user, LOGIN_PROVIDER, REFRESH_TOKEN, request.RefreshToken);
            if (isValidRefreshToken)
            {
                var token = await _tokenGenerator.GenerateTokenAsync(_user);
                return new AuthResponseDto
                {
                    Token = token,
                    UserId = _user.Id,
                    RefreshToken = await CreateRefreshToken()
                };
            }
            await _userManager.UpdateSecurityStampAsync(_user);
            return null;
        }

        //private async Task<string> GenerateTokenAsync(ApiUser user)
        //{
        //    // Create the Key
        //    var securityKey = new SymmetricSecurityKey(
        //        Encoding.UTF8.GetBytes(_config["JwtSettings:Key"])
        //    );

        //    // Create Credentials
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    // Get the roles that the user has (Admin .. User)
        //    var roles = await _userManager.GetRolesAsync(user);

        //    var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
        //    var userClaims = await _userManager.GetClaimsAsync(user);

        //    var claims = new List<Claim>
        //    {
        //        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        //        new Claim("UserId", user.Id)

        //    }.Union(roleClaims).Union(userClaims);

        //    var token = new JwtSecurityToken(
        //        issuer: _config["JwtSettings:Issuer"],
        //        audience: _config["JwtSettings:Audience"],
        //        claims: claims,
        //        expires: DateTime.Now.AddMinutes(Convert.ToInt32(_config["JwtSettings:DurationInMinutes"])),
        //        signingCredentials: credentials
        //        );

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
    }
}

using HotelListing.API.Contracts;
using HotelListing.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace HotelListing.API.Repository
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _config;
        private readonly UserManager<ApiUser> _userManager;
        private readonly SymmetricSecurityKey _securityKey;

        public TokenGenerator(IConfiguration config, UserManager<ApiUser> userManager)
        {
            _config = config;
            _userManager = userManager;
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
        }

        public async Task<string> GenerateTokenAsync(ApiUser user)
        {

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: await GenerateClaims(user),
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_config["JwtSettings:DurationInMinutes"])),
                signingCredentials: CreateCredentils()
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<IEnumerable<Claim>> GenerateClaims(ApiUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("UserId", user.Id)

            }.Union(roleClaims).Union(userClaims);

            return claims;
        }

        private SigningCredentials CreateCredentils()
        {
            return new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
        }







        public string GenerateToken(string username, string email, IEnumerable<string> roles)
        {
            // Create a list of claims for the token
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Email, email)
        };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Set the token expiration time
            DateTime expires = DateTime.UtcNow.AddHours(1);

            // Create a JWT token
            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256)
            );

            // Serialize the token to a string
            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

        
    }
}

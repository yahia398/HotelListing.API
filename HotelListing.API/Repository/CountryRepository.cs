using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Models;
using HotelListing.API.Models.ModelsDto.ApiUser;
using HotelListing.API.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Repository
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        private readonly AppDbContext _db;
        public CountryRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Country?> GetWithDetailsAsync(int id)
        {
            var country = await _db.Countries
                .Include(q => q.Hotels)
                .SingleOrDefaultAsync(c => c.Id == id);

            return country;
        }

        public void Update(Country entity)
        {
            _db.Countries.Update(entity);
        }
    }

    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;

        public AuthManager(IMapper mapper, UserManager<ApiUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
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

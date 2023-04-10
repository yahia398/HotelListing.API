using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Data
{
    public class ApiUser : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}

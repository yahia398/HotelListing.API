using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Core.Models.ModelsDto.ApiUser
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.ModelsDto.ApiUser
{
    public class ApiUserDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set;}

    }
}

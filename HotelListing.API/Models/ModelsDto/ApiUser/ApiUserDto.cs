using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.ModelsDto.ApiUser
{
    public class ApiUserDto : LoginDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set;}

    }
}

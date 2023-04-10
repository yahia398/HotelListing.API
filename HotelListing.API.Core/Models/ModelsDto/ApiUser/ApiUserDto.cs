using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Core.Models.ModelsDto.ApiUser
{
    public class ApiUserDto : LoginDto
    {
        [Required]
        public required string UserName { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        [Phone]
        public required string PhoneNumber { get; set;}

    }
}

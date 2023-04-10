using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Core.Models.ModelsDto.Hotel
{
    public abstract class BaseHotelDto
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Address { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public int CountryId { get; set; }
    }
}

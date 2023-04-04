using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.API.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Address { get; set; }

        public int Rating { get; set; }

        public int CountryId { get; set; }

        public Country? Country { get; set; }

    }
}

namespace HotelListing.API.Models
{
    public class Country
    {

        public int Id { get; set; }

        public required string Name { get; set; }

        public required string CountryCode { get; set; }

        public virtual IList<Hotel>? Hotels { get; set; }
    }
}
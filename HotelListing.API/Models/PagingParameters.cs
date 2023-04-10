using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HotelListing.API.Models
{
    public class PagingParameters
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

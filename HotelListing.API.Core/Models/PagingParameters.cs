﻿
namespace HotelListing.API.Core.Models
{
    public class PagingParameters
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

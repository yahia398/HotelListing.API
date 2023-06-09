﻿using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Core.Models.ModelsDto.Country
{
    public abstract class CountryBaseDto
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string CountryCode { get; set; }

    }
}

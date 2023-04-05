﻿using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.ModelsDto.ApiUser
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

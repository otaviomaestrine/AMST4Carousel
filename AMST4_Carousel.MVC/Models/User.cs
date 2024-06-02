﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMST4_Carousel.MVC.Models
{
    public class User : IdentityUser
    {
        public string Adress { get; set; } = string.Empty;

        public string ZipCode { get; set; } = string.Empty;  

        public string City { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
        [NotMapped]

        public string ImageUrl { get; set; } = string.Empty;
        

        public bool IsActive { get; set; }

        public DateTime BirthDate { get; set;}

       
    }
}

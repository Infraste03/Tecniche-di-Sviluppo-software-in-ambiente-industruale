using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sito.Models
{
    public class Login
    {
        [Required]
        public string email{get; set;}
        [Required]
        public string password { get; set; }

    }
}
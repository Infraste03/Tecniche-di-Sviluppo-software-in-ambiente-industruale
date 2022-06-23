using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sito.Models
{
    public class Registrazione
    {
        [Required]
        public string nome { get; set; }
        [Required]
        public string cognome { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string carta_credito { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sito.Models
{
    public class Prenotazione
    {
        [Required]
        [Range(1, 3)]
        [RegularExpression("([1-9][0-9]*)")]
        public int idcrociera { get; set; }
        [Required]
        [Range  (1,5000)]
        [RegularExpression("([1-9][0-9]*)",ErrorMessage = "inserire solo valori interi ")]
        public int num_biglietti { get; set; }

        public List<Crociere> listaCrociere { get; set; }
    }
}
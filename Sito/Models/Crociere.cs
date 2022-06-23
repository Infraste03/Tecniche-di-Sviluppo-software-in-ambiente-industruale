using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sito.Models
{
    public class Crociere
    {
        public int id_crociera { get; set; }

        public string nome_nave { get; set; }

        public string localita { get; set; }

        public string data { get; set; }

        public int numero_persone { get; set; }

        public double prezzo { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto.Classi
{
    public class Utente
    {
       
        public string nome { get; set; }

        public string cognome { get; set; }

        public bool is_admin { get; set; }

        public string email { get; set; }

        public string password { get; set; }
        public string carta_credito { get; set; }

    }
}

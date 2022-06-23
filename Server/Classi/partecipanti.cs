using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto.Classi
{
    public class Partecipanti
    {
        public int fk_crociera { get; set; }

        public int fk_utente { get; set; }

        public int Numero_Biglietti { get; set; }
    }
}

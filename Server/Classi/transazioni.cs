using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto.Classi
{
    public class Transazioni
    {
        public int id_transazione { get; set; }

        public string importo { get; set; }

        public int fk_utente { get; set; }

        public int fk_barca { get; set; }

        public string data { get; set; }

        public int Numero_Tickets { get; set; }

    }
}

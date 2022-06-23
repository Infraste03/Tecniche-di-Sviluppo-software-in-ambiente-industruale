using Progetto.Classi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Progetto
{
    //interfaccia del  server 
    [ServiceContract]

    public interface IAgenzia
    {
        [OperationContract]
        Utente registrazione(Utente utente);

        [OperationContract]
        Utente Login(Utente utente);
        [OperationContract]
        bool Prenotazione(Utente u, int idcrociera, int part);
        [OperationContract]
        List<Crociere> visualizzaCrociere();
        [OperationContract]
        List<Partecipanti> visualizzaPartecipanti();

        [OperationContract]
        List<Transazioni> visualizzaTransazioni();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MySql.Data.MySqlClient;
using Progetto.Classi;

namespace Progetto
{   
    public class Agenzia : IAgenzia //servizi che il server fornisce 
    {  
        //Funzione che permette all' utente di registrarsi
        public Utente registrazione(Utente user)
        {
            try
            {   //connessione al database dbcruise
                MySqlConnection conn = null;
                var connectionString = "server=localhost;database=dbcruise;uid=root;pwd='';";
                conn = new MySqlConnection(connectionString);
                conn.Open();
                Console.WriteLine("Connessione sql aperta");
                using (MySqlCommand command1 = conn.CreateCommand())
                {   
                    //Controllo per verificare che non esista già un altro utente con la stessa mail
                    command1.CommandText = "select * from utente where email = '" + user.email + "';";
                    var resultSet = command1.ExecuteReader();
                    Console.WriteLine("check email già presente");

                    if (resultSet.Read())
                    {
                        Console.WriteLine("utente già presente" + resultSet);

                        //conn.Close();
                        throw new Exception("utente già presente");
                    }
                }
                conn.Close();
                conn.Open();
                using (MySqlCommand command2 = conn.CreateCommand())
                {
                    //Query che permette ad un nuovo utente di registrarsi inserendo i dati richiesti
                    command2.CommandText = "INSERT INTO utente (nome,cognome,is_admin,email,password,carta_credito) VALUES ('"+user.nome+"','"+user.cognome+"','"+0+"','"+user.email+"','"+user.password+"','"+user.carta_credito+"')";
                    var resultSet = command2.ExecuteNonQuery();
                    Console.WriteLine("ExecutenonQuery");
                    if (resultSet > 0)
                    {
                        Console.WriteLine("Registrazione ok");
                        conn.Close();
                        return user;

                    }
                    else
                    {
                        throw new Exception("Registrazione fallita");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
        public Utente Login(Utente user)
        {
            //Connessione al DB
            MySqlConnection conn = null;
            var connectionString = "server=localhost;database=dbcruise;uid=root;pwd='';";
            conn = new MySqlConnection(connectionString);
            conn.Open();
            Console.WriteLine("Login in corso");
            try
            {
                using (MySqlCommand command1 = conn.CreateCommand())
                {
                    //Query per effettuare il login
                    command1.CommandText = "select * from utente where email = '" + user.email + "' AND password = '" + user.password + "';";
                    var resultSet = command1.ExecuteReader();
                    
                    if (resultSet.Read())
                    {
                        //Condizione per verificare che l' utente che effettua il login sia o meno admin
                        int is_admin = Convert.ToInt32(resultSet["is_admin"]);
                        if (is_admin == 1)
                        {
                            user.is_admin = true;
                        }
                        else user.is_admin = false;
                        
                        return user;
                    }
                    else
                    {
                        throw new Exception("Login fallito");
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
        public bool Prenotazione(Utente u,int idcrociera,int part)
        {
            int id_utente;
            int partmax;
            double prezzo;
            Console.WriteLine(u.email);
            //Connessione al DB
            MySqlConnection conn = null;
            var connectionString = "server=localhost;database=dbcruise;uid=root;pwd='';";
            conn = new MySqlConnection(connectionString);
            conn.Open();
            //Creazione di una transazione che viene associata al DB
            MySqlTransaction myTrans;
            myTrans = conn.BeginTransaction();
            Console.WriteLine("Prenotazione in corso");
            try
            {
                using (MySqlCommand command2 = conn.CreateCommand())
                {
                    //La transazione prende dalla tabella utente, l' id associato alla mail dell' utente loggato in quel momento
                    command2.CommandText = "select * from utente where email = '" + u.email + "';";
                    var resultSet = command2.ExecuteReader();
                    
                    if (resultSet.Read())
                    {
                        //Conversione in int 
                        id_utente = Convert.ToInt32(resultSet["id_utente"]);
                        resultSet.Close();
                    }
                    else
                    {
                        throw new Exception("Prenotazione fallita");
                    }
                }
                using (MySqlCommand command2 = conn.CreateCommand())
                {
                    //La transazione prende l' id della crociera 
                    command2.CommandText = "select * from crociere where id_crociera = '" + idcrociera + "';";
                    var resultSet = command2.ExecuteReader();
                    
                    if (resultSet.Read())
                    {
                        //gli altri due paramentri che servono per la transazione sono il numero di persone massimo e il prezzo della crociera
                        partmax = Convert.ToInt32(resultSet["numero_persone"]);
                        prezzo = Convert.ToDouble(resultSet["prezzo"]);
                        resultSet.Close();
                    }
                    else
                    {
                        throw new Exception("Prenotazione fallita");
                    }
                }
                using (MySqlCommand command3 = conn.CreateCommand())
                {
                    command3.CommandText = "select Numero_Biglietti from partecipanti where fk_crociera = '" + idcrociera + "';";
                    var resultSet = command3.ExecuteReader();
                    
                    //creazione di una lista di partecipanti
                    List<int> listapartecipanti = new List<int>();
                    while (resultSet.Read())
                    {
                        //Vengono aggiunti nella lista appena creata tutti i biglietti associati ai partecipanti scorrendo riga per riga 
                        listapartecipanti.Add(Convert.ToInt32(resultSet["Numero_Biglietti"]));

                    }
                    resultSet.Close();
                    //controllo del num dei partecipanti 
                    if (listapartecipanti.Sum() + part > partmax)
                    {
                        throw new Exception("Non ci sono posti a sufficienza");

                    }
                }
                // Start a local transaction               
                using (MySqlCommand command2 = conn.CreateCommand())
                {
                    //dopo i controlli vengono aggiunti alla tabella partecipanti i nuovi partecipanti
                    command2.Transaction = myTrans;
                    command2.CommandText = "INSERT INTO partecipanti (fk_crociera,fk_utente,Numero_Biglietti) VALUES ('" + idcrociera + "','" + id_utente + "','" + part + "')";
                    var resultSet = command2.ExecuteNonQuery();
                    Console.WriteLine("ExecutenonQuery");
                    if (resultSet > 0)
                    {
                        Console.WriteLine("Partecipanti ok");

                    }
                    else
                    {
                        throw new Exception("Impossibile inserire partecipante");
                    }
                }

                using (MySqlCommand command2 = conn.CreateCommand())
                {
                    //inserimento della nuova transazione nella tabella transazione
                    command2.Transaction = myTrans;
                    command2.CommandText = "INSERT INTO transazioni (importo,fk_utente,fk_barca,Numero_Tickets) VALUES ('" + prezzo*part + "','" + id_utente + "','" + idcrociera + "','" + part + "')";
                    var resultSet = command2.ExecuteNonQuery();
                    Console.WriteLine("ExecutenonQuery");
                    if (resultSet > 0)
                    {
                        Console.WriteLine("transazione ok");

                    }
                    else
                    {
                        throw new Exception("Transazione fallita");
                    }
                }
                //Se tutti gli insert sono ok viene salvato nel DB il nuovo stato
                myTrans.Commit();
                conn.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                //nel caso in cui una delle precedenti operaioni fallisca, allora il DB rimane invariato
                myTrans.Rollback();
                conn.Close();
                return (false);
            }
        }
        public List<Crociere> visualizzaCrociere()
        {
            var connectionString = "server=localhost;database=dbcruise;uid=root;pwd='';";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                try
                {
                    using (MySqlCommand command2 = conn.CreateCommand())
                    {
                        //creazione lista crociere 
                        List<Crociere> listacrociere = new List<Crociere>();
                        command2.CommandText = "select id_crociera,nome_nave,localita,DATE_FORMAT(data,'%d-%m-%Y') as data, numero_persone, prezzo from crociere;";
                        var resultSet = command2.ExecuteReader();
                        
                        while (resultSet.Read())
                        {
                            //Leggendo riga per riga aggiunge i dati nuovi della corciera e gli aggiunge alla lista crociere
                            Crociere crociera= new Crociere();
                            crociera.id_crociera = Convert.ToInt32(resultSet["id_crociera"]);
                            crociera.nome_nave = Convert.ToString(resultSet["nome_nave"]);
                            crociera.localita = Convert.ToString(resultSet["localita"]);
                            crociera.data = Convert.ToString(resultSet["data"]);
                            crociera.numero_persone = Convert.ToInt32(resultSet["numero_persone"]);
                            crociera.prezzo = Convert.ToDouble(resultSet["prezzo"]);
                            listacrociere.Add(crociera);
                            
                        }
                        resultSet.Close();
                        //Se non ci sono crociere allora viene generata un eccezione 
                        if (listacrociere.Count()==0)
                        {
                            throw new Exception("Impossibile visualizzare la lista: è vuota");
                        }
                        return listacrociere;
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                    return null;
                }
            }
        }
        public List<Transazioni> visualizzaTransazioni()
        {
            var connectionString = "server=localhost;database=dbcruise;uid=root;pwd='';";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                try
                {
                    using (MySqlCommand command2 = conn.CreateCommand())
                    {
                        //creazione lista transazione 
                        List<Transazioni> listatransazioni = new List<Transazioni>();
                        command2.CommandText = "select id_transazione,importo,fk_utente,fk_barca, DATE_FORMAT(data,'%d-%m-%Y') as data, Numero_Tickets from transazioni;";
                        var resultSet = command2.ExecuteReader();
                        
                        while (resultSet.Read())
                        {
                            //Leggendo riga per riga aggiunge i dati nuovi della trasazione e gli aggiunge alla lista transazioni
                            Transazioni trans = new Transazioni();
                            
                            trans.id_transazione = Convert.ToInt32(resultSet["id_transazione"]);
                            trans.importo = Convert.ToString(resultSet["importo"]);
                            trans.fk_utente = Convert.ToInt32(resultSet["fk_utente"]);
                            trans.fk_barca = Convert.ToInt32(resultSet["fk_barca"]);
                            trans.data = Convert.ToString(resultSet["data"]);
                            trans.Numero_Tickets = Convert.ToInt32(resultSet["Numero_Tickets"]);

                            listatransazioni.Add(trans);

                        }
                        resultSet.Close();
                        //Se non ci sono transazioni allora viene generata un eccezione 
                        if (listatransazioni.Count() == 0)
                        {
                            throw new Exception("Impossibile visualizzare le transazioni");
                        }
                        return listatransazioni;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return null;
                }
            }
        }

        public List<Partecipanti> visualizzaPartecipanti()
        {
            var connectionString = "server=localhost;database=dbcruise;uid=root;pwd='';";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                try
                {
                    using (MySqlCommand command2 = conn.CreateCommand())
                    {
                        //creazione lista partecipanti 
                        List<Partecipanti> listapartecipanti = new List<Partecipanti>();
                        command2.CommandText = "select fk_crociera,fk_utente,Numero_Biglietti from partecipanti;";
                        var resultSet = command2.ExecuteReader();
                        
                        while (resultSet.Read())
                        {
                            //Leggendo riga per riga aggiunge i dati nuovi dei partecipanti e gli aggiunge alla lista partecipanti
                            Partecipanti pa = new Partecipanti();
                            
                            pa.fk_crociera = Convert.ToInt32(resultSet["fk_crociera"]);
                            pa.fk_utente = Convert.ToInt32(resultSet["fk_utente"]);
                            pa.Numero_Biglietti = Convert.ToInt32(resultSet["Numero_Biglietti"]);
                            listapartecipanti.Add(pa);

                        }
                        resultSet.Close();
                        //Se non ci sono partecipanti allora viene generata un eccezione 
                        if (listapartecipanti.Count() == 0)
                        {
                            throw new Exception("Impossibile visualizzare lista partecipanti");
                        }
                        return listapartecipanti;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return null;
                }
            }
        }
    }
}

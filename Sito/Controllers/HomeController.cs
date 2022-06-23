using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Windows;
using Sito.Models;
using Sito.ServiceReference1;



namespace Sito.Controllers
{
    public class HomeController : Controller
    {
        public static ServiceReference1.AgenziaClient wcf = new AgenziaClient();

        //Nella pagina di Index viene istanziata la connessione al server
        public ActionResult Index()
        {
            try
            {
                wcf = new ServiceReference1.AgenziaClient();
                return View();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("LogOnError", e.Message);
                return View();
            }
            
        }

        //Funzione che permette all' admin nella sua sezione di visualizzare le varie tabelle
        public ActionResult Sezione_Admin()
        {
            try
            {
                ViewBag.Crociere = wcf.visualizzaCrociere();
                if (ViewBag.Crociere == null) throw new Exception("Visualizzazione crociere fallita");
                ViewBag.Partecipanti = wcf.visualizzaPartecipanti();
                if (ViewBag.Crociere == null) throw new Exception("Visualizzazione partecipabti fallita");
                ViewBag.Transazioni = wcf.visualizzaTransazioni();
                if (ViewBag.Crociere == null) throw new Exception("Visualizzazione transazioni fallita");

                return View();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("LogOnError", e.Message);
                return RedirectToAction("Index");
            }
            
        }
        public ActionResult Logout()
        {
            Session["utenteAttivo"] = null;
            Session.Abandon();
            MessageBox.Show("LOGOUT effettuato");
            return RedirectToAction("Index");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login utente)
        {
            try
            {
                //Pagina nel quale l' utente inserisce i dati per il login
                string h = "Login effettuato correttamente!";
                Utente ut = new Utente();
                ut.email = utente.email;
                ut.password = utente.password;
                var risultato = wcf.Login(ut);
                if (risultato == null) throw new Exception("Login fallito");
                Session["utenteAttivo"] = risultato;
                
                if (risultato.is_admin == true)
                {
                    return RedirectToAction("Sezione_Admin");
                }
                else
                {
                    MessageBox.Show(h);
                    return RedirectToAction("Index");
                }

            }
            catch (Exception e)
            {
                ModelState.AddModelError("LogOnError", e.Message);
                return View();
            }
            
        }

        public ActionResult Registrazione()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registrazione(Registrazione utente) //gestione submit della pagina 
        {
            try
            {
                //pagina nella quale l' utente inserisce i dati per la registrazione
                string l = "Regitrazione avvenuta con successo!";
                Utente ut = new Utente();
                ut.email = utente.email;
                ut.password = utente.password;
                ut.nome = utente.nome;
                ut.cognome = utente.cognome;
                ut.carta_credito = utente.carta_credito;
                var risultato = wcf.registrazione(ut);
                if (risultato == null) throw new Exception("Registrazione fallita");
                Session["utenteAttivo"] = risultato;
                MessageBox.Show(l);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("LogOnError", e.Message);
                return View();
            }
        }
        public ActionResult Prenotazione()
        {
            try
            {

                Prenotazione p = new Prenotazione();
                p.listaCrociere = new List<Models.Crociere>();
                var lista = wcf.visualizzaCrociere().ToList();
                

                foreach (var l in lista)
                {
                    Models.Crociere temp = new Models.Crociere();
                    temp.id_crociera = l.id_crociera;
                    temp.localita = l.localita;
                    temp.nome_nave = l.nome_nave;
                    temp.numero_persone = l.numero_persone;
                    temp.data = l.data;
                    temp.prezzo = l.prezzo;
                    p.listaCrociere.Add(temp);                  
                }
               
                if ((String)Session["LogOnError"] != null)
                {
                    ModelState.AddModelError("LogOnError", (String)Session["LogOnError"]);
                    Session["LogOnError"] = null;
                }
                if (p.listaCrociere == null) throw new Exception("Visualizzazione fallita");
               
                return View(p);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("LogOnError", e.Message);
                return RedirectToAction("Index");
            }         
        }
        [HttpPost]

        public ActionResult Prenotazione(Prenotazione partecipanti)
        {
            string m = "Prenotazione avvenuta con successo! Goditi la tua esperienza :)";
            try
            {
                //Pagina in cui l' utente può prenotarsi 
                Utente ut = (Utente)Session["utenteAttivo"];
                

                var risultato = wcf.Prenotazione(ut,partecipanti.idcrociera,partecipanti.num_biglietti);
                if (!risultato)
                {
                    throw new Exception("Prenotazione fallita");
                }
                MessageBox.Show(m);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("LogOnError", e.Message);
                Session["LogOnError"]= e.Message;
                return RedirectToAction("Prenotazione");
                
            }

        }
        public ActionResult About()
        {
            ViewBag.Message = "Informazioni relative ai meravigliosi autori del sito :-) .";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Contatti";

            return View();
        }
    }
}
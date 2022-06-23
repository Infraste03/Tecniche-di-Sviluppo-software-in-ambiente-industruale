using Progetto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var svc = new ServiceHost(typeof(Agenzia));
            svc.Open();
            Console.WriteLine("servizio wcf aperto");
            Console.ReadLine();
            svc.Close();
        }
    }
}

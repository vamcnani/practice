using EvalRestServiceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(EvalRestService));

            //EndPointConfiguration(host);

            try
            {
                host.Open();
                PrintServiceInfo(host);
                Console.ReadLine();
                host.Close();
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                host.Abort();
            }

        }

        private static void EndPointConfiguration(ServiceHost host)
        {
            host.AddServiceEndpoint(typeof(IEvalRestService),
                new BasicHttpBinding(),
                "http://localhost:8080/evals/basic");

            host.AddServiceEndpoint(typeof(IEvalRestService),
                new WSHttpBinding(),
                "http://localhost:8080/evals/ws");

            host.AddServiceEndpoint(typeof(IEvalRestService),
                new NetTcpBinding(),
                "net.tcp://localhost:8081/evals");
        }

        static void PrintServiceInfo(ServiceHost host)
        {
            Console.WriteLine("{0} is up and running with these endpoints:",
                host.Description.ServiceType);
            foreach (var se in host.Description.Endpoints)
            {
                Console.WriteLine(se.Address);
            }
        }
    }
}

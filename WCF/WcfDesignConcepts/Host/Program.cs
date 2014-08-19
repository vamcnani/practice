using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MathServiceLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(MathService));
            try
            {
                host.Open();
                Console.WriteLine("Math service is running ...!");
                Console.ReadLine();
                host.Close();
            }
            catch (Exception e)
            {
                host.Abort();
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
}

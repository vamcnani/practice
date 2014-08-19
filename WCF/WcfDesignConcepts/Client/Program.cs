using Client.MathServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathServiceLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            MathClient client = new MathClient("BasicHttpBinding_IMath");
            client.Open();
            Console.WriteLine("{0}", client.Add(56, 556));
            Console.ReadLine();
                       
            client.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleServiceContracts
{
    public class MyService : IFoo, IBar, IBaz
    {
        public string SaySomethingNice(string name)
        {
            return string.Format("{0} is cool!", name);
        }

        public string SayGoodBye(string name)
        {
            return string.Format("Bye {0}!", name);
        }

        public string SayHello(string name)
        {
            return string.Format("Hello {0}!", name);
        }
    }
}

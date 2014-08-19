using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MathServiceLibrary
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class MathService : IMath
    {
        public double Add(double x, double y)
        {
            return x + y;
        }

        public double Subtract(double x, double y)
        {
            return x - y;
        }

        public double Multiple(double x, double y)
        {
            return x * y;
        }

        public double Divide(double x, double y)
        {
            return x / y;
        }
    }
}

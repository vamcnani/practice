using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MathServiceLibrary
{
    [ServiceContract]
    public interface IMath
    {
        [OperationContract]
        double Add(double x, double y);


        [OperationContract]
        double Subtract(double x, double y);


        [OperationContract]
        double Multiple(double x, double y);


        [OperationContract]
        double Divide(double x, double y);
    }
}

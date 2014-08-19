using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MultipleServiceContracts
{

    [ServiceContract]
    public interface IBar:IFoo
    {
        [OperationContract]
        string SayGoodBye(string name);
    }
}
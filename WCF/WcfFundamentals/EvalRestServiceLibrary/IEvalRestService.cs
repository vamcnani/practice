using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace EvalRestServiceLibrary
{
    [ServiceContract]
    public interface IEvalRestService
    {
        [OperationContract]
        void SubmitEval(Eval eval);
        [OperationContract]
        List<Eval> GetEvals();
    }

}

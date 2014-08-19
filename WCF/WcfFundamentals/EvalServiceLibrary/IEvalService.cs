using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace EvalServiceLibrary
{
    [ServiceContract]
    public interface IEvalService
    {
        [OperationContract]
        void SubmitEval(Eval eval);
        [OperationContract]
        List<Eval> GetEvals();
    }

}

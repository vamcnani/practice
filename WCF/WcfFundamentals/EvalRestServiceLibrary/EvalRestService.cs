using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace EvalRestServiceLibrary
{   
    [ServiceBehavior(InstanceContextMode= InstanceContextMode.Single)]
    public class EvalRestService:IEvalRestService
    {
        List<Eval> evals = new List<Eval>();

        public void SubmitEval(Eval eval)
        {
            if (eval.Submitter.Equals("Throw"))
                throw new FaultException("Error with SubmitVal");
            evals.Add(eval);
        }

        public List<Eval> GetEvals()
        {
            //System.Threading.Thread.Sleep(5000);
            return evals;
        }
    }
}

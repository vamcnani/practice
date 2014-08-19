using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EvalRestServiceLibrary
{
    [DataContract]
    public class Eval
    {
        [DataMember]
        public string Submitter;
        [DataMember]
        public DateTime TimeSent;
        [DataMember]
        public string Comments;
    }
}

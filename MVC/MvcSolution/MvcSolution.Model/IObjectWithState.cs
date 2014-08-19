using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcSolution.Model
{
    public interface IObjectWithState
    {
        ObjectState ObjectState { get; set; }
        
    }

    public enum ObjectState
    {
        Unchanged=0,
        Added=1,
        Modified=2,
        Deleted=3        
    }
}

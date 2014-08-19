using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqOperators.Repositories
{
    public class DepartmentRepository
    {
        public IEnumerable<Deparment> GetAll()
        {           
            return _departments;
        }

        List<Deparment> _departments = new List<Deparment>()
        {
             new Deparment { ID=1, Name="Engineering" },
             new Deparment { ID=2, Name="Sales" },
             new Deparment { ID=3, Name="Management" }
        };

    }
}

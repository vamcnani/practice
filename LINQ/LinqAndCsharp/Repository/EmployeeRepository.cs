using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqAndCsharp.Repository
{
    public class EmployeeRepository
    {
        List<Employee> _employees = new List<Employee>
        {
            new Employee(){ID=1,Name="Vamsi",DepartmentID=1 },
            new Employee(){ID=2,Name="Jyoshna",DepartmentID=1 },
            new Employee(){ID=8,Name="Raja",DepartmentID=2 },
            new Employee(){ID=5,Name="Lokesh",DepartmentID=2 },
            new Employee(){ID=12,Name="Ravi",DepartmentID=2 },
        };

        public IEnumerable<Employee> GetAll()
        {
            return _employees;
        }
    }
}

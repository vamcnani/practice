using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqOperators.Repositories
{
    public class EmployeeRepository
    {
        public IEnumerable<Employee> GetAll()
        {
            return _employees;
        }

        public void Add(Employee e)
        {
            _employees.Add(e);
        }

        public IEnumerable<Employee> GetByDepartmentID(int id)
        {
            return
                from e in _employees
                where e.DepartmentID == id
                select e;
        }

        List<Employee> _employees = new List<Employee>
        {
            new Employee() { ID=1, Name="Scott", DepartmentID=1},
            new Employee() { ID=2, Name="Poonam", DepartmentID=1 },
            new Employee() { ID=3, Name="Linda", DepartmentID=1 },
            new Employee() { ID=4, Name="Paul", DepartmentID=1 },
            new Employee() { ID=5, Name="Igor", DepartmentID=2 },
            new Employee() { ID=6, Name="Anna", DepartmentID=2 }
        };                            
    }
}

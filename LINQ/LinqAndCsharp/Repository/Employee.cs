using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqAndCsharp.Repository
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime HireDate { get; set; }
        public int DepartmentID { get; set; }        

        public static Employee[] GetEmployees()
        {
            return new Employee[]
            {
            new Employee { ID = 1, Name = "Vamsi", HireDate = new DateTime(2003, 05, 07) , DepartmentID=1},
            new Employee { ID = 2, Name = "Jyoshna", HireDate = new DateTime(2004, 05, 11) ,DepartmentID=1},
            new Employee { ID = 3, Name = "Loks", HireDate= new DateTime(2001,03,18),DepartmentID=2},
            new Employee { ID = 4, Name = "Samrat", HireDate = new DateTime(2000, 08, 13), DepartmentID=2 }
            };
        }

        //public static List<Employee> GetEmployeeList()
        //{

        //    return 0;
        //}
    }
}

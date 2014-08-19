using LinqQueries.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LinqQueries
{
    class Program
    {
        static void Main(string[] args)
        {
            UseLetKeyword();
            UseIntoKeyword();
            UseGroupByKeyword();
            UseGroupByCompositeKey();
            UseGroupByNewObject();
            UseJoinKeyword();
            UseGroupJoinKeyword();
            UseCrossJoin();
            Console.ReadLine();
        }

        private static void UseCrossJoin()
        {
            var employeeRepository = new EmployeeRepository();
            var departmentRespository = new DepartmentRepository();
            var quey = from e in employeeRepository.GetAll()
                       from d in departmentRespository.GetAll()
                       select new { EName = e.Name, DName = d.Name };
            Console.WriteLine("\nCross Join");
            foreach (var item in quey)
            {
                Console.WriteLine("#{0}:{1}",item.EName,item.DName);
            }


        }

        private static void UseGroupJoinKeyword()
        {
            //Group join is similar to Left join in SQL
            var employeeRepository = new EmployeeRepository();
            var departmentRespository = new DepartmentRepository();
            var query = from d in departmentRespository.GetAll()
                        join e in employeeRepository.GetAll()
                        on d.ID equals e.DepartmentID
                        into Dep
                        select new
                        {
                            Name = d.Name,
                            Employees=Dep
                        };
            Console.WriteLine("\nGroup Join Keyword");
            foreach (var item in query)
            {
                Console.WriteLine("<{0}>",item.Name);
                foreach (var emp in item.Employees)
                {
                    Console.WriteLine("{0}:{1}", emp.ID, emp.Name);    
                }
                

            }
        }

        private static void UseJoinKeyword()
        {
            var employeeRepository = new EmployeeRepository();
            var departmentRespository = new DepartmentRepository();
            var query = from d in departmentRespository.GetAll()
                        join e in employeeRepository.GetAll()
                        on d.ID equals e.DepartmentID
                        select new
                        {
                            Employee = e.Name,
                            Department = d.Name
                        };
            Console.WriteLine("\nJoin Keyword");
            foreach (var item in query)
            {
                Console.WriteLine("{0}:{1}",item.Department,item.Employee);
                
            }

        }

        #region Grouping Keywords

        private static void UseGroupByNewObject()
        {
            var repository = new EmployeeRepository();
            var queryByFirstLetter =
                from employee in repository.GetAll()
                group employee by employee.Name[0] into letterGroup
                orderby letterGroup.Key ascending
                select new
                {
                    FirstLetter = letterGroup.Key,
                    Count= letterGroup.Count(),
                    Employees=letterGroup
                };
          
            Console.WriteLine("\nUseGroupByNewObject");
            foreach (var group in queryByFirstLetter)
            {
                Console.WriteLine("FirstLetter={0}:Count={1}",group.FirstLetter,group.Count);
                foreach (var employee in group.Employees)
                {
                    Console.WriteLine("\tDID={0}:Name={1}", employee.DepartmentID, employee.Name);
                }

            }

            #region MethodSyntax
            var queryByFirstLetter2 = repository.GetAll()
              .GroupBy(e => e.Name[0])
              .OrderBy(e => e.Key)
              .Select(g =>
                      new
                      {
                          FirstLetter = g.Key,
                          Count = g.Count(),
                          Employees = g
                      });
            Console.WriteLine("\nUseGroupByNewObject:Method Syntax");
            foreach (var group in queryByFirstLetter)
            {
                Console.WriteLine("FirstLetter={0}:Count={1}", group.FirstLetter, group.Count);
                foreach (var employee in group.Employees)
                {
                    Console.WriteLine("\tDID={0}:Name={1}", employee.DepartmentID, employee.Name);
                }

            }
            #endregion
        }

        private static void UseGroupByCompositeKey()
        {
            var repository = new EmployeeRepository();
            var groupedEmployees =
                from employee in repository.GetAll()
                group employee by new { employee.DepartmentID, FirstLetter = employee.Name[0] };
            Console.WriteLine("\nUseGroupByCompositeKey");
            foreach (var group in groupedEmployees)
            {
                Console.WriteLine("{0}:{1}", group.Key.DepartmentID, group.Key.FirstLetter);
                foreach (var employee in group)
                {
                    Console.WriteLine("\t{0}:{1}", employee.DepartmentID, employee.Name);
                }

            }
        }

        private static void UseGroupByKeyword()
        {
            var repository = new EmployeeRepository();
            var groupedEmployees =
                from employee in repository.GetAll()
                group employee by employee.Name[0] into letterGroup
                orderby letterGroup.Key ascending
                select letterGroup;

            foreach (var group in groupedEmployees)
            {
                Console.WriteLine(group.Key);
                foreach (var employee in group)
                {
                    Console.WriteLine("\t{0}:{1}", employee.DepartmentID, employee.Name);
                }

            }
        }

        private static void UseIntoKeyword()
        {
            var repository = new EmployeeRepository();
            var query = from employee in repository.GetAll()
                        where employee.Name.StartsWith("P")
                        select employee
                            into pEmployee
                            where pEmployee.Name.Length < 5
                            select pEmployee;
            foreach (var emp in query)
            {
                Console.WriteLine(emp.Name);

            }
        }

        private static void UseLetKeyword()
        {
            var repository = new EmployeeRepository();
            var query =
                from e in repository.GetAll()
                let lname = e.Name.ToLower()
                where lname == "scott"
                select lname;

            foreach (var emp in query)
            {
                Console.WriteLine(emp);

            }
        }
        #endregion
    }
}

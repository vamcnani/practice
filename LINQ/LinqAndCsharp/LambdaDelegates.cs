using LinqAndCsharp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqAndCsharp
{
    public static class LambdaDelegates
    {
        private static void LambdaSyntax(Employee[] employees)
        {
            Employee emp = Array.Find(employees, e => e.Name.EndsWith("t"));
            Console.WriteLine("Lambda Syntax:" + emp.Name);
        }

        private static void DelegateSyntax(Employee[] employees)
        {
            Employee emp = Array.Find(employees, delegate(Employee e)
            {
                return e.Name == "Jyoshna";
            });
            Console.WriteLine("Delegate Syntax:" + emp.Name);
        }

        private static void PredicateSyntax(Employee[] employees)
        {
            Employee emp = Array.Find(employees, FindEmployee);
            Console.WriteLine("Predicate Syntax:" + emp.Name);
        }

        static bool FindEmployee(Employee e)
        {
            return e.Name == "Loks";
        }

        public static void Execute()
        {
            Employee[] employees = Employee.GetEmployees();
            PredicateSyntax(employees);
            DelegateSyntax(employees);
            LambdaSyntax(employees);
        }
    }
}

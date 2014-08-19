using LinqOperators.Repositories;
using LinqQueries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqOperators
{
    class Program
    {
        static void Main(string[] args)
        {
            Filtering();
            Sorting();
            SetOperations();
            Projection();
            //Partioning();
            Joins();

        }

        private static void Joins()
        {
            var employees = new EmployeeRepository().GetAll();
            var departments = new DepartmentRepository().GetAll();

            var query1 = from d in departments
                         join e in employees
                         on d.ID equals e.DepartmentID
                         select new
                         {
                             DepartmentName = d.Name,
                             EmployeeName = e.Name
                         };

            var query2= departments
                .Join(employees,
                d=>d.ID,
                e=>e.DepartmentID,
                (d,e)=>new{
                    DeparmentName=d.Name,
                    EmployeeName=e.Name
                });

            var groupQuery = departments
                .GroupJoin(employees,
                d => d.ID,
                e => e.DepartmentID,
                (d, eg) => new
                {
                    DeparmentName = d.Name,
                    EmployeeName = eg
                });

        }            

        private static void Partioning()
        {
            throw new NotImplementedException();
        }

        private static void Projection()
        {
            string[] famousQuotes =
            {
                "Advertising is legalized lying",
                "Advertising is the greatest art form of the twentieth century"
            };

            var query = famousQuotes.Select(s => s.Split(' ')).Distinct();
            var query2 = famousQuotes.SelectMany(s => s.Split(' ')).Distinct();
        }

        private static void SetOperations()
        {
            int[] twos = { 2, 4, 6, 8, 10 };
            int[] threes = { 3, 6, 9, 12, 15 };

            var intersect=twos.Intersect(threes);
            var except = twos.Except(threes);
            var union = twos.Union(threes);
            var contact = twos.Concat(threes);

            var books = new List<Book>
            {
                new Book{Author="scott", Name="Programming WF" },
                new Book{Author="Fritz", Name="Programming ASP.NET" },
                new Book{Author="scott", Name="Programming WF" },
            };

            var query = books.Distinct(); //this gives 3 items
            //below query gives 2 items
            var query2 = books
                .Select(b => new { b.Name, b.Author })
                .Distinct();
         }

        private static void Sorting()
        {
            var query= Process.GetProcesses()
                .OrderBy(p=> p.WorkingSet64)
                .ThenByDescending(p=>p.Threads.Count);
        }

        private static void Filtering()
        {
            object[] things= {"Computers",45,"Books"};
            ArrayList listOfThings = new ArrayList(things);
            var query = listOfThings.OfType<int>();
        }
    }
}

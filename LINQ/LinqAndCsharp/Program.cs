using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqAndCsharp.Repository;

namespace LinqAndCsharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //LambdaDelegates.Execute();
            //FuncAndActionDelegates.Execute();
            //LambdaExpressions.Execute();
            ////DisplayMovies();
            //MethodSyntax();
            var query = new EmployeeRepository().GetAll()
                .Where(e => e.DepartmentID < 6 && e.ID < 7)
                .OrderByDescending(e => e.DepartmentID)
                .OrderBy(e => e.Name);
                
            
            foreach (var emp in query)
            {
                Console.WriteLine(emp.Name);                
            }

            Console.ReadLine();
        }

        private static void MethodSyntax()
        {
            IEnumerable<Employee> employees = Employee.GetEmployees();
            var query = employees.Where(c => c.Name.EndsWith("s"))
                                .OrderBy(c => c.Name)
                                .Select(c => c);
            foreach (var emp in query)
            {
                Console.WriteLine(emp.Name);
            }
            Console.ReadLine();
        }

        private static void DisplayMovies()
        {
            MoviesDataContext dc = new MoviesDataContext();
            IEnumerable<Movie> movies = dc.Movies.Where(m => m.ReleaseDate.Year == 2005);
            foreach (var movie in movies)
            {
                Console.WriteLine("Title:{0},Review:{1}", movie.Title, movie.ReleaseDate);
            }

        }

    }
}

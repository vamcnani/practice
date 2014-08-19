using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinqAndCsharp
{
    class LambdaExpressions
    {
        public static void Execute()
        {
            ActionDemo();
            FuncDemo();
        }

        private static void FuncDemo()
        {
            Expression<Func<int, int, string>> Multiply = (x, y) => string.Format("Multiplicaiton Result:{0}", x * y);

            Func<int, int, string> Result = Multiply.Compile();
            Console.WriteLine("\nUsing Funcs..Lambda expressions..");
            
            Console.WriteLine(Result(5, 8).ToString());
        }

        private static void ActionDemo()
        {
            Expression<Action<int, int>> printTwoNumbers = (x, y) => Console.WriteLine(string.Format("{0}\t{1}", x, y));
            Action<int, int> Result = printTwoNumbers.Compile();

            Console.WriteLine("\nPrinting numbers using Lambda expressions....");

            Result(125, 231);
        }
    }
}

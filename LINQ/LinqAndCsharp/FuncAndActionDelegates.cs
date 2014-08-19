using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqAndCsharp
{
    class FuncAndActionDelegates
    {
        public static void Execute()
        {
            ActionDemo();
            FuncDemo();
        }

        private static void FuncDemo()
        {
            Func<int> ReturnsInteger = () => 5;
            Func<DateTime> getDateTime = () => DateTime.Now;
            Func<int, int> Square = (x) => x * x;
            Func<int, int, string> Multiply = (x, y) =>  string.Format("Multiplicaiton Result:{0}",x*y);

            Console.WriteLine("\nUsing Funcs......");
            Console.WriteLine(ReturnsInteger().ToString());
            Console.WriteLine(getDateTime().ToString());
            Console.WriteLine(Square(5).ToString());
            Console.WriteLine(Multiply(5,8).ToString());
        }

        private static void ActionDemo()
        {
            Action printHello = () => Console.WriteLine("Hello");
            Action getDate = () => Console.WriteLine(DateTime.Now);
            Action<int> printNumber = (x) => Console.WriteLine(x);
            Action<int, int> printTwoNumbers = (x, y) => Console.WriteLine(string.Format("{0}\t{1}", x, y));
              
            Console.WriteLine("\nPrinting numbers using Action delegates....");
            printHello();
            getDate();
            printNumber(55);
            printTwoNumbers(125, 231);
        }

    }
}

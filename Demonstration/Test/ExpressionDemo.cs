using RationalTypes;
using System;

namespace Demonstration
{
    internal class ExpressionDemo
    {
        public static void Interactive()
        {
            Console.CancelKeyPress += (object x,ConsoleCancelEventArgs y) => { System.Environment.Exit(0); };
            while (true)
            {
                try
                {
                    Console.WriteLine("Výraz ke spočtení:");
                    string s = Console.ReadLine();
                    Console.WriteLine(" = ");
                    Console.WriteLine(RationalTypes.Expression.Evaluate(s));
                }
                catch (Exception)
                {
                    Console.WriteLine("Program nedokáže výraz vyhodnotit.");
                }
            }
        }

        public static void Demo()
        {
            Next(@"7+7*7*7+7/7/7");
            Next(@"512/8");
            Next(@"(5)(59*756)(56(8/(4)))");
            Next(@"1+1/(1+1/(1+1/(1+1/(1+1/(1+1/(1+1))))))");

            return;

            void Next(string s) { Console.WriteLine(String.Concat(s, "\t=\t", Expression.Evaluate(s))); }
        }
    }
}
using System;
using System.Numerics;
using RationalTypes;

namespace Demonstration
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ;
            Console.Read();
        }

        private static void Example1()
        {
            var r1 = (Rational)43789 / 132;
            var r2 = (Rational)4 / 9;
            var i1 = 5;
            Console.WriteLine(i1 * r2.Pow(2) - r1.Inverse / 7);
            Console.WriteLine((i1 * r2.Pow(2) - r1.Inverse / 7).DecimalApproximation);
        }

        private static void Example2()
        {
            var r1 = (BigRational)489495648945 / 512315498405;
            var r2 = (BigRational)4563194 / 8213156489;
            var i1 = 5454561234;
            Console.WriteLine(i1 * r2.Pow(11) - r1.Inverse / 7);
            Console.WriteLine((i1 * r2.Pow(11) - r1.Inverse / 7).DecimalApproximation);
        }

        private static void Example3()
        {
            var p = new RationalPolynomial(new Rational[]
            {
                (Rational) 56,
                (Rational) 616/10,
                -(Rational) 38976/100,
                (Rational)28224/100
            });

            Console.WriteLine(p.ToString());
            foreach (var r in HornerScheme.GetRationalRoots(p.MakePolynomial()))
            { Console.Write(r + " "); }
            Console.WriteLine();
        }
    }
}
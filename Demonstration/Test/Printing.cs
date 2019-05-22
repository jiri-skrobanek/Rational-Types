using System;
using System.Collections.Generic;
using System.Text;
using RationalTypes;

namespace Demonstration
{
    class Printing
    {
        public static void Demo()
        {
            var v1 = new RationalTypes.Rational(1561384532, 456456378);
            var v2 = new RationalTypes.Rational(789, 756);
            Print(v1);
            Print(v2);
            Print(v1 + v2);
            Print(v1 - v2);
            Print(v1 * v2);
            Print(v1 / v2);
        }

        static void Print(RationalTypes.Rational r)
        {
            Console.WriteLine(String.Concat(r.ToString(), "\t", r.ToSignedString(), "\t", r.DecimalApproximation));
        }
    }
}

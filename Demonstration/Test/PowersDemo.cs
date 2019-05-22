using RationalTypes;
using System;

namespace Demonstration
{
    internal class PowersDemo
    {
        public static void Demo()
        {
            // Jde:

            Next((Rational)1 / 37, (Rational)2);
            Next((Rational)4096 / 81, (Rational)1 / 2);
            Next(-(Rational)1024 / 243, (Rational)3 / 5);
            Next((Rational)1024 / 4, (Rational)0);
            Next(-(Rational)(49 * 49 * 49) / (81 * 81 * 81), (Rational)1 / 3);

            // Nejde:

            Next(-(Rational)762 / 567, (Rational)8 / 7);
            Next((Rational)5, (Rational)35);
            Next((Rational)0, -(Rational)66565);

            return;

            void Next(Rational num, Rational exp)
            {
                Console.WriteLine("(" + num.ToString() + ")^(" + exp + "):");
                try
                {
                    Console.WriteLine(num.Pow(exp));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public static void HighestDemo()
        {
            Rational r;
            r = new Rational(1, 37);
            Console.WriteLine(r.ToString() + ":");
            Console.WriteLine(r.HighestPower());
            r = new Rational(4096, 81);
            Console.WriteLine(r.ToString() + ":");
            Console.WriteLine(r.HighestPower());
            r = new Rational(-1024, 243);
            Console.WriteLine(r.ToString() + ":");
            Console.WriteLine(r.HighestPower());
            r = new Rational(1024, 4);
            Console.WriteLine(r.ToString() + ":");
            Console.WriteLine(r.HighestPower());
            r = new Rational(-49 * 49 * 49, 81 * 81 * 81);
            Console.WriteLine(r.ToString() + ":");
            Console.WriteLine(r.HighestPower());
        }
    }
}
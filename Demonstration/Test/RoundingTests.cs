using System;

namespace Demonstration
{
    internal class RoundingTests
    {
        public static void Test()
        {
            Test(new RationalTypes.Rational(70, 25));
            Test(new RationalTypes.Rational(-70, 25));
            Test(new RationalTypes.Rational(410, 15));
            Test(new RationalTypes.Rational(-410, 15));
            Test(new RationalTypes.Rational(0));
            Test(new RationalTypes.Rational(-6, 4));
            Test(new RationalTypes.Rational(6, 4));
        }

        private static void Test(RationalTypes.Rational r)
        {
            RationalTypes.BigRational br = (RationalTypes.BigRational)r;

            Console.WriteLine(r.ToString());
            Console.WriteLine(r.GetFloor + "\t" + br.GetFloor);
            Console.WriteLine(r.GetCeiling + "\t" + br.GetCeiling);
            Console.WriteLine(r.GetTruncate + "\t" + br.GetTruncate);
            Console.WriteLine(r.GetStretch + "\t" + br.GetStretch);
            Console.WriteLine(r.GetRound + "\t" + br.GetRound + "\r\n");
        }
    }
}
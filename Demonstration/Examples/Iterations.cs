using System;

namespace Demonstration
{
    /// <summary>
    /// Demonstration of the loss of precision during iteration, also timed version to compare to rationals.
    /// </summary>
    internal static class Iterations
    {
        public static double FloatIteration(int count)
        {
            DateTime dt = DateTime.Now;
            double state = 0.4; int i = 0;
            while (i++ < count)
            {
                state *= 7.0;
                state /= 2.0;
                state -= 1.0;
            }
            Console.WriteLine((DateTime.Now - dt).Milliseconds);
            return state;
        }

        public static RationalTypes.Rational RationalIteration(int count)
        {
            DateTime dt = DateTime.Now;
            RationalTypes.Rational state = (RationalTypes.Rational)2 / 5; int i = 0;
            while (i++ < count)
            {
                state *= 7;
                state /= 2;
                state -= 1;
            }
            Console.WriteLine((DateTime.Now - dt).Milliseconds);
            return state;
        }

        public static void Time()
        {
            int count = 100000;
            DateTime dt = DateTime.Now;
            for (int j = 0; j < 10; j++)
            {
                double state = 0.4; int i = 0;
                while (i++ < count)
                {
                    state *= 7.0;
                    state /= 2.0;
                    state -= 1.0;
                }
            }
            Console.WriteLine((DateTime.Now - dt).TotalSeconds);

            dt = DateTime.Now;
            for (int j = 0; j < 10; j++)
            {
                RationalTypes.Rational state = (RationalTypes.Rational)2 / 5; int i = 0;
                while (i++ < count)
                {
                    state *= 7;
                    state /= 2;
                    state -= 1;
                }
            }
            Console.WriteLine((DateTime.Now - dt).TotalSeconds);
        }
    }
}
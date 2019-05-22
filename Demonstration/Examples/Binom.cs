using System;
using System.Collections.Generic;

namespace Demonstration
{
    internal static class Binom
    {
        private static List<double> _computed = new List<double> { 1 };
        private static List<double> _computedLogs = new List<double> { 0.0 };

        public static double Factorial(byte arg)
        {
            int len = _computed.Count;
            while (len <= arg)
            {
                _computed.Add(len * _computed[len++ - 1]);
            }
            return _computed[arg];
        }

        public static double LogOfFactorial(short arg)
        {
            int len = _computedLogs.Count;
            while (len <= arg)
            {
                _computedLogs.Add(Math.Log(len) + _computedLogs[len++ - 1]);
            }
            return _computed[arg];
        }

        public static double GAvg(params double[] numbers)
        {
            double product = 0.0;
            foreach (var num in numbers)
            {
                product += Math.Log(num);
            }
            /////
            Console.WriteLine("e^" + product);
            /////
            return Math.Exp(product / numbers.Length);
        }

        public static void Test()
        {
            Console.WriteLine("Počet čísel:");
            var count = uint.Parse(Console.ReadLine());
            Random r = new Random();
            double[] nums = new double[count];
            for (int i = 0; i < count; i++)
            {
                nums[i] = r.NextDouble() * (r.Next() % 0x1000);
            }
            //Console.WriteLine("Násobením: " + GAvgSimple(nums));
            Console.WriteLine("Logaritmováním: " + GAvg(nums));
            Console.Read();
        }
    }
}
using System;

namespace Demonstration
{
    internal static class GeometricAverage
    {
        public static double GAvgSimple(params double[] numbers)
        {
            double product = 1.0;
            foreach (var num in numbers)
            {
                product *= num;
            }
            /////
            Console.WriteLine(product.ToString());
            /////
            return Math.Pow(product, 1.0 / numbers.Length);
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
            Console.WriteLine("Násobením: " + GAvgSimple(nums));
            Console.WriteLine("Logaritmováním: " + GAvg(nums));
            Console.Read();
        }
    }
}
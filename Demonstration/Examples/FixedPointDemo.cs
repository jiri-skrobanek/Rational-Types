namespace Demonstration
{
    internal class FixedPointDemo
    {
        public static void Compute()
        {
            var n1 = new FixedPointNumber(15, 0x80000000, false);
            var n2 = new FixedPointNumber(3, 0, true);
            var n3 = FixedPointNumber.Times(n1, n2);
        }
    }
}
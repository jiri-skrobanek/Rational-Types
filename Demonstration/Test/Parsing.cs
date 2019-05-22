using RationalTypes;
using System;

namespace Demonstration
{
    public static class Parsing
    {
        public static void ParsingTest()
        {
            Parse("165/456512");
            Parse("456123498/156456456465465");
            Parse("-45654/-45616");
            Parse("-45648974");
            Parse("0");
            Parse("");
            Parse("456d/5645");
            Parse("1111111111111111111111111111111111111");
        }

        private static void Parse(string s)
        {
            try
            {
                Rational.Parse(s);
                Console.WriteLine("OK");
            }
            catch (Exception e)
            {
                Console.WriteLine("Fail - " + e.Message);
            }
        }
    }
}
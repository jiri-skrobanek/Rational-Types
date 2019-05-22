/********************************************************************
 * The RationalTypes .NET Core Library
 * Copyright (C) 2018 Jiří Škrobánek
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 ********************************************************************/

using System;
using System.Collections.Generic;
using System.Numerics;

namespace RationalTypes
{
    public static class Utility
    {
        /// <summary>
        /// Provides good approximation of Ludolf's number.
        /// </summary>
        public static readonly Rational pi = new Rational(355, 113);

        /// <summary>
        /// Provides good approximation of Euler's number.
        /// </summary>
        public static readonly Rational e = new Rational(169, 39);

        /// <summary>
        /// List of primes under 64.
        /// </summary>
        public static readonly int[] SmallPrimes = new int[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61 };

        /// <summary>
        /// Calculates the floor of square root of a long integer.
        /// </summary>
        /// <param name="Number">Number to take square root from</param>
        /// <returns>Floor of the square root </returns>
        public static long IntegralPartSquareRoot(long Number)
        {
            if (Number < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            long l = 0, r = Number;
            long i = (Number + 1) / 2;
            while (l < r)
            {
                if (i * i > Number)
                {
                    r = i - 1;
                }
                else if (i * i < Number)
                {
                    l = i;
                    if (i * i + 2 * i + 1 > Number)
                    {
                        return i;
                    }
                }
                else
                {
                    return i;
                }

                i = (l + r + 1) / 2;
            }

            return i;
        }

        /// <summary>
        /// Calculates the floor of Nth root of a long integer.
        /// </summary>
        /// <param name="Number">Number to take Nth root from</param>
        /// <returns>Floor of the Nth root </returns>
        public static long IntegralPartNthRoot(long Number, long N)
        {
            if (Number < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            long l = 0, r = Number;
            long i = (Number + 1) / 2;
            while (l < r)
            {
                if (i * i > Number)
                {
                    r = i - 1;
                }
                else if (Pow(i, N) < Number)
                {
                    l = i;
                    if (Pow(i + 1, N) > Number)
                    {
                        return i;
                    }
                }
                else
                {
                    return i;
                }

                i = (l + r + 1) / 2;
            }

            return i;
        }

        /// <summary>
        /// To the power of function. (Use with nonnegative exponent only.)
        /// </summary>
        /// <param name="Base">Base</param>
        /// <param name="Exponent">Nonnegative exponent</param>
        /// <returns>Base the the power of Exponent</returns>
        public static long Pow(long Base, long Exponent)
        {
            checked
            {
                long res = 1;
                for (; Exponent-- > 0;)
                {
                    res *= Base;
                }
                return res;
            }
        }

        /// <summary>
        /// Returns true if the number is a square of an integer.
        /// </summary>
        /// <param name="Number">Suspected square</param>
        /// <returns>If the number is perfect square</returns>
        public static bool IsPerfectSquare(long Number)
        {
            if (Number < 0)
            {
                return false;
            }

            long l = IntegralPartSquareRoot(Number);
            return l * l == Number;
        }

        /// <summary>
        /// Determines whether supplied number is a square of rational number.
        /// </summary>
        /// <param name="Number">Suspected square</param>
        /// <returns>If the number is perfect square</returns>
        public static bool IsPerfectSquare(Rational Number)
        {
            if (Number.Denominator < 0 || Number.Numerator < 0)
            {
                return false;
            }

            long l = IntegralPartSquareRoot(Number.Numerator);
            long m = IntegralPartSquareRoot(Number.Denominator);
            return l * l == Number.Numerator && m * m == Number.Denominator;
        }

        /// <summary>
        /// Returns the lowest common multiple of array of numbers.
        /// </summary>
        /// <param name="Numbers">Array of arguments</param>
        /// <returns>The lowest common multiple</returns>
        public static long LCM(params long[] Numbers)
        {
            if (Numbers.Length == 0)
            {
                throw new ArgumentException();
            }
            else if (Numbers.Length == 1)
            {
                return Numbers[0];
            }

            long temp = Numbers[0];
            for (int i = 1; i < Numbers.Length; i++)
            {
                temp = LCM(temp, Numbers[i]);
            }
            return temp;
        }

        /// <summary>
        /// Returns the lowest common multiple of two numbers. Or whichever number is nonzero.
        /// </summary>
        public static long LCM(long a, long b)
        {
            checked
            {
                if (a == 0 || b == 0)
                {
                    return Math.Abs(a + b);
                }
                return Math.Abs(a * b / GCD(a, b));
            }
        }

        /// <summary>
        /// Returns the lowest common multiple of two numbers. Or whichever number is nonzero.
        /// </summary>
        public static BigInteger BigLCM(BigInteger a, BigInteger b)
        {
            if (a == 0 || b == 0)
            {
                return BigInteger.Abs(a + b);
            }

            return BigInteger.Abs(a * b / BigGCD(a, b));
        }

        /// <summary>
        /// Returns the greatest common divisor of numbers.
        /// Computed by Euclidean algorithm.
        /// </summary>
        /// <param name="Numbers"></param>
        /// <returns>Largest common divisor</returns>
        public static long GDCE(params long[] Numbers)
        {
            if (Numbers.Length == 1)
            {
                return Math.Abs(Numbers[0]);
            }
            if (Numbers.Length == 0)
            {
                throw new ArgumentException();
            }

            long a = 1;
            int i = 0;
            for (; i < Numbers.Length; i++)
            {
                if (Numbers[i] != 0)
                {
                    a = Math.Abs(Numbers[i]);
                    i++;
                    break;
                }
            }

            for (; i < Numbers.Length; i++)
            {
                long b = Math.Abs(Numbers[i]);
                while (b != 0)
                {
                    a = a % b;

                    long c = b;
                    b = a;
                    a = c;
                }
            }

            return a;
        }

        public static BigInteger BigGCD(params BigInteger[] Numbers)
        {
            if (Numbers.Length == 1)
            {
                return BigInteger.Abs(Numbers[0]);
            }
            if (Numbers.Length == 0)
            {
                throw new ArgumentException();
            }

            BigInteger a = 1;
            int i = 0;
            for (; i < Numbers.Length; i++)
            {
                if (Numbers[i] != 0)
                {
                    a = BigInteger.Abs(Numbers[i]);
                    i++;
                    break;
                }
            }

            for (; i < Numbers.Length; i++)
            {
                BigInteger b = BigInteger.Abs(Numbers[i]);
                while (b != 0)
                {
                    a = a % b;

                    BigInteger c = b;
                    b = a;
                    a = c;
                }
            }

            return a;
        }

        /// <summary>
        /// Returns the greatest common divisor of numbers.
        /// Computed by binary GCD algorithm.
        /// </summary>
        /// <returns>Largest common divisor</returns>
        public static long GCD(params long[] Numbers)
        {
            if (Numbers.Length == 0)
            { throw new ArgumentException(); }

            long x = Numbers[0];
            for (int i = 1; i < Numbers.Length; i++)
            {
                x = gcd(x, Numbers[i]);
            }
            if (x == 0)
            {
                // When all arguments are zeroes.
                return 1;
            }
            return x;

            long gcd(long u, long v)
            {
                if (u < 0)
                {
                    u *= -1;
                }

                if (v < 0)
                {
                    v *= -1;
                }

                if (u == v) return u;

                if (u == 0) return v;

                if (v == 0) return u;

                if ((~u & 1) > 0)
                {
                    if ((v & 1) > 0) return gcd(u >> 1, v);
                    else return gcd(u >> 1, v >> 1) << 1;
                }

                if ((~v & 1) > 0) return gcd(u, v >> 1);

                // reduce larger argument
                if (u > v) return gcd((u - v) >> 1, v);

                return gcd((v - u) >> 1, u);
            }
        }

        /// <summary>
        /// Calculates good rational approximation of a double value
        /// </summary>
        /// <returns>Approximation</returns>
        public static Rational GoodApproximation(double D)
        {
            int exp = 1; ulong mul = 2;
            while (D >= mul)
            {
                exp++;
                mul <<= 1;
            }
            mul = 1;
            while (1 / D >= mul)
            {
                exp--;
                mul <<= 1;
            }

            List<double> rem = new List<double>();
            List<long> ps = new List<long>(), qs = new List<long>();

            rem.Add(Math.Pow(2, exp));
            rem.Add(D);
            ps.Add(1); ps.Add(0);
            qs.Add(0); qs.Add(1);

            int i = 1;
            while (qs[i] < 10000 && qs[i] > -10000)
            {
                long t = (long)(rem[i - 1] / rem[i]);

                rem.Add(rem[i - 1] - rem[i] * t);
                qs.Add(qs[i - 1] - t * qs[i]);
                ps.Add(ps[i - 1] - t * ps[i]);
                i++;
            }
            if (exp > 0)
            {
                ps[i] <<= exp;
            }
            else if (exp < 0)
            {
                qs[i] <<= -exp;
            }

            return new Rational(D < 0 ? -Math.Abs(ps[i]) : Math.Abs(ps[i]), Math.Abs(qs[i]));
        }

        /// <summary>
        /// Conversion of <see cref="RationalTypes.Rational"/> to <see cref="RationalTypes.BigRational"/>.
        /// </summary>
        public static BigRational MakeBigRational(this Rational r)
        {
            return new RationalTypes.BigRational(r.Numerator, r.Denominator);
        }
    }
}
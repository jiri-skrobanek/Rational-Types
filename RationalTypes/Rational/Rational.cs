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

using RationalTypes.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using static RationalTypes.Utility;

namespace RationalTypes
{
    /// <summary>
    /// Represents a rational number.
    /// </summary>
    public partial struct Rational : IRational
    {
        private long _numerator, _denominator;

        public long Numerator
        {
            get
            {
                return _numerator;
            }
            set
            {
                long gcd = GCD(value, _denominator);
                _numerator = value / gcd;
                _denominator /= gcd;
            }
        }

        public long Denominator
        {
            get
            {
                return _denominator;
            }
            set
            {
                if (value > 0)
                {
                    long gcd = GCD(value, _numerator);
                    _numerator /= gcd;
                    _denominator = value / gcd;
                }
                else if (value == 0)
                {
                    throw new DivideByZeroException("Zero denominator");
                }
                else
                {
                    // Always keep denominator positive.
                    long gcd = GCD(value, _numerator);
                    _numerator /= -gcd;
                    _denominator = -value / gcd;
                }
            }
        }

        #region Constructors

        public Rational(long Number)
        {
            _numerator = Number;
            _denominator = 1;
        }

        public Rational(long Numerator, long Denominator)
        {
            if (Denominator == 0)
            {
                throw new DivideByZeroException("Zero denominator.");
            }

            _numerator = Numerator;
            _denominator = Denominator;
            this.Denominator = Denominator;
        }

        #endregion Constructors

        public override bool Equals(object obj)
        {
            if (obj is Rational o)
            {
                return o._denominator == _denominator && o._numerator == _numerator;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public string ToTeXString()
        {
            if (Denominator == 1)
            {
                return Numerator.ToString();
            }

            if (Numerator == 0)
            {
                return "0";
            }

            return String.Concat("\\frac{", Numerator.ToString(), "}{", Denominator.ToString(), "}");
        }

        public string ToSignedTeXString()
        {
            if (Denominator == 1)
            {
                return Numerator.ToSignedString();
            }

            if (Numerator == 0)
            {
                return "+0";
            }

            return String.Concat(Sgn == -1 ? "-" : "+", "\\frac{", Math.Abs(Numerator).ToString(), "}{", Denominator.ToString(), "}");
        }

        public override string ToString()
        {
            if (Denominator == 1)
            {
                return Numerator.ToString();
            }

            if (Numerator == 0)
            {
                return "0";
            }

            return String.Concat(Numerator.ToString(), "/", Denominator.ToString());
        }

        public string ToSignedString()
        {
            if (this < 0)
            {
                return ToString();
            }
            else
            {
                return "+" + ToString();
            }
        }

        /// <summary>
        /// Converts the string representation of a number to its <see cref="RationalTypes.Rational"/> representation.
        /// </summary>
        public static Rational Parse(string Representation)
        {
            var parts = Representation.Split('/');
            if (parts.Length == 2)
            {
                if (long.TryParse(parts[0], out long nom) && long.TryParse(parts[1], out long den))
                {
                    return new Rational(nom, den);
                }
                else
                {
                    throw new FormatException("Not a valid rational number.");
                }
            }
            else if (parts.Length == 1)
            {
                if (long.TryParse(parts[0], out long result))
                {
                    return new Rational(result);
                }
                else
                {
                    throw new FormatException("Not a valid rational number.");
                }
            }
            else
            {
                throw new FormatException("Not a valid rational number.");
            }
        }

        public double DoublePrecisionValue
        {
            get
            {
                return (double)_numerator / (double)_denominator;
            }
        }

        public float SinglePrecisionValue
        {
            get
            {
                return (float)_numerator / (float)_denominator;
            }
        }

        public string DecimalApproximation
        {
            get
            {
                string ret = string.Empty;
                var bytes = Approximation(10, 5);
                if (bytes[0] == 1)
                {
                    ret += "-";
                }
                foreach (byte b in bytes.Skip(1))
                {
                    if (b == 255)
                    {
                        ret += ",";
                    }
                    else
                    {
                        ret += b.ToString();
                    }
                }
                return ret;
            }
        }

        public Rational Pow(long Exponent)
        {
            if (Exponent == 0)
            {
                return (Rational)0;
            }
            else if (Exponent < 0)
            {
                return Pow(-Exponent).Inverse;
            }
            else
            {
                return new Rational(Utility.Pow(this.Numerator, Exponent), Utility.Pow(this.Denominator, Exponent));
            }
        }

        public byte[] Approximation(byte Base, int precision)
        {
            if (Base < 2 || Base == 255) { throw new ArgumentException("Invalid base."); }

            long num = Math.Abs(Numerator);
            byte[] approx;
            int index = 1;

            if (num >= Denominator)
            {
                long whole = num / Denominator;

                int exp = 0; long mul = 1;
                for (int i = 0; whole > mul * (Base - 1); i++)
                {
                    mul *= Base; exp++;
                }

                approx = new byte[precision + exp + 3];
                if (Numerator < 0)
                {
                    approx[0] = 1; // -
                }
                else
                {
                    approx[0] = 0; // +
                }

                while (exp-- >= 0)
                {
                    approx[index++] = (byte)(whole / mul);
                    whole %= mul; mul /= Base;
                }
            }
            else
            {
                approx = new byte[precision + 3];
                if (Numerator < 0)
                {
                    approx[0] = 1; // -
                }
                else
                {
                    approx[0] = 0; // +
                }
                approx[index++] = 0;
            }

            approx[index++] = 255; // ,

            long dec = num % Denominator;
            while (precision-- > 0)
            {
                dec *= Base;
                approx[index++] = (byte)(dec / Denominator);
                dec %= Denominator;
            }
            return approx;
        }

        int IComparable.CompareTo(object ob)
        {
            if (ob is Rational r)
            {
                if (this < r)
                {
                    return -1;
                }
                else if (this == r)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else if (ob is int i)
            {
                if (this < i)
                {
                    return -1;
                }
                else if (this == i)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else if (ob is long l)
            {
                if (this < l)
                {
                    return -1;
                }
                else if (this == l)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                throw new InvalidOperationException("Not comparable.");
            }
        }

        /// <summary>
        /// Absolute value function
        /// </summary>
        /// <returns>The absolute value of this <see cref="Rational"/></returns>
        public Rational Abs()
        {
            return (this > 0) ? this : -this;
        }

        public static explicit operator Rational(long l)
        {
            return new Rational(l);
        }

        /// <summary>
        /// Replaces the number with simpler approximation.
        /// </summary>
        /// <param name="MaxDenominator">Maximal value of denominator</param>
        public void Simplify(long MaxDenominator)
        {
            List<long> rem = new List<long>();
            List<long> ps = new List<long>(), qs = new List<long>();

            rem.Add(_numerator);
            rem.Add(_denominator);
            ps.Add(1); ps.Add(0);
            qs.Add(0); qs.Add(1);

            int i = 1;
            while (qs[i] < Math.Abs(MaxDenominator) && qs[i] > -Math.Abs(MaxDenominator))
            {
                long t = (long)(rem[i - 1] / rem[i]);

                rem.Add(rem[i - 1] - rem[i] * t);
                qs.Add(qs[i - 1] - t * qs[i]);
                ps.Add(ps[i - 1] - t * ps[i]);
                i++;
            }

            _numerator = _numerator > 0 ? Math.Abs(qs[i - 1]) : -Math.Abs(qs[i - 1]);
            Denominator = Math.Abs(ps[i - 1]);
        }

        /// <summary>
        /// Returns the multiplicative inverse.
        /// </summary>
        public Rational Inverse
        {
            get
            {
                if (_denominator == 0)
                {
                    throw new DivideByZeroException("This number cannot be inverted.");
                }

                return new Rational(_denominator, _numerator);
            }
        }

        /// <summary>
        /// Returns the largest exponent e, such that q^e = this, where q is <see cref="Rational"/>.
        /// </summary>
        /// <returns>Exponent</returns>
        public int HighestPower()
        {
            int res = 1; long _n = Math.Abs(Numerator), _d = Denominator;

            if (_n == 0 || (_n == 1 && _d == 1))
            {
                return int.MaxValue;
            }

            for (; ; )
            {
                Next:

                foreach (int p in SmallPrimes)
                {
                    bool a = Utility.Pow(binarySearchPower(_n, p), p) == _n;
                    bool b = Utility.Pow(binarySearchPower(_d, p), p) == _d;
                    if (a && b)
                    {
                        res *= p;
                        _n = binarySearchPower(Math.Abs(_n), p);
                        _d = binarySearchPower(Math.Abs(_d), p);
                        goto Next;
                    }
                }
                break;
            }

            if (Numerator < 0)
            {
                // Negative number cannot be even power.
                while ((res & 1) == 0)
                {
                    res = res >> 1;
                }
            }
            return res;

            long binarySearchPower(long n, long p)
            {
                long l = 0, r = n;
                long i = (n + 1) / 2;
                while (l < r)
                {
                    try
                    {
                        if (Utility.Pow(i, p) > n)
                        {
                            r = i - 1;
                        }
                        else if (Utility.Pow(i, p) < n)
                        {
                            l = i;
                            /*if (i * i + 2 * i + 1 > n)
                            {
                                return i;
                            }*/
                        }
                        else
                        {
                            return i;
                        }
                    }
                    catch
                    {
                        r = i - 1;
                    }
                    finally
                    {
                        i = (l + r + 1) / 2;
                    }
                }

                return i;
            }
        }

        public Rational Pow(Rational Exponent)
        {
            if (Exponent <= 0 && this == 0)
            {
                throw new DivideByZeroException();
            }
            else if (Exponent == 0)
            {
                if (this == 0)
                {
                    throw new DivideByZeroException();
                }
                return (Rational)1;
            }
            else if (this == 0)
            {
                return (Rational)0;
            }
            else if (this < 0 && (Exponent.Denominator & 1) == 0)
            {
                throw new ComplexResultException();
            }
            long l = 0, r = Math.Abs(Numerator); long i = (Math.Abs(Numerator) + 1) / 2;
            long _n = 0, _d = 0;
            while (l < r)
            {
                try
                {
                    if (Utility.Pow(i, Exponent.Denominator) > Math.Abs(Numerator))
                    { r = i - 1; }
                    else if (Utility.Pow(i, Exponent.Denominator) < Math.Abs(Numerator))
                    { l = i; }
                    else
                    { _n = i; break; }
                }
                catch
                { r = i - 1; }
                finally
                { i = (l + r + 1) / 2; }
            }
            if (_n == 0)
            {
                throw new IrrationalResultException();
            }
            l = 0; r = Denominator; i = (Denominator + 1) / 2;
            while (l < r)
            {
                try
                {
                    if (Utility.Pow(i, Exponent.Denominator) > Denominator)
                    { r = i - 1; }
                    else if (Utility.Pow(i, Exponent.Denominator) < Denominator)
                    { l = i; }
                    else
                    { _d = i; break; }
                }
                catch
                { r = i - 1; }
                finally
                { i = (l + r + 1) / 2; }
            }
            if (_d == 0)
            {
                throw new IrrationalResultException();
            }
            var res = new Rational(Utility.Pow(Sgn * _n, Math.Abs(Exponent.Numerator)), Utility.Pow(_d, Math.Abs(Exponent.Numerator)));
            if (Exponent > 0)
            {
                return res;
            }
            else
            {
                return res.Inverse;
            }
        }

        /// <summary>
        /// The signum function
        /// </summary>
        public SByte Sgn
        {
            get
            {
                if (this > 0) { return 1; }
                else if (this < 0) { return -1; }
                else
                {
                    return 0;
                }
            }
        }
    }
}
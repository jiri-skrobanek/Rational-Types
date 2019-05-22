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
using System.Linq;
using System.Numerics;
using static RationalTypes.Utility;

namespace RationalTypes
{
    /// <summary>
    /// Represents rational number with high range of values.
    /// </summary>
    public partial struct BigRational : IRational
    {
        private BigInteger _numerator, _denominator;

        public BigInteger Numerator
        {
            get
            {
                return _numerator;
            }
            set
            {
                BigInteger gcd = BigGCD(value, _denominator);
                _numerator = value / gcd;
                _denominator /= gcd;
            }
        }

        public BigInteger Denominator
        {
            get
            {
                return _denominator;
            }
            set
            {
                if (value > 0)
                {
                    BigInteger gcd = BigGCD(value, _numerator);
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
                    BigInteger gcd = BigGCD(value, _denominator);
                    _numerator /= -gcd;
                    _denominator = -value / gcd;
                }
            }
        }

        #region Constructors

        public BigRational(long Number)
        {
            _numerator = Number;
            _denominator = 1;
        }

        public BigRational(BigInteger Number)
        {
            _numerator = Number;
            _denominator = 1;
        }

        public BigRational(long Numerator, long Denominator)
        {
            if (Denominator == 0)
            {
                throw new DivideByZeroException("Zero denominator");
            }

            _numerator = Numerator;
            _denominator = Denominator;
            this.Denominator = Denominator;
        }

        public BigRational(BigInteger Numerator, BigInteger Denominator)
        {
            if (Denominator == 0)
            {
                throw new DivideByZeroException("Zero denominator");
            }

            _numerator = Numerator;
            _denominator = Denominator;
            this.Denominator = Denominator;
        }

        #endregion Constructors

        public override bool Equals(object obj)
        {
            if (obj is BigRational b)
            {
                return b._denominator == _denominator && b._numerator == _numerator;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Provides TEX notation of the value.
        /// </summary>
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

            return String.Concat(Sgn == -1 ? "-" : "+", "\\frac{", BigInteger.Abs(Numerator).ToString(), "}{", Denominator.ToString(), "}");
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
        /// Converts the string representation of a number to its <see cref="RationalTypes.BigRational"/> representation.
        /// </summary>
        public static BigRational Parse(string Representation)
        {
            var parts = Representation.Split('/');
            if (parts.Length == 2)
            {
                if (BigInteger.TryParse(parts[0], out BigInteger nom) && BigInteger.TryParse(parts[1], out BigInteger den))
                {
                    return new BigRational(nom, den);
                }
                else
                {
                    throw new FormatException("Not a valid rational number.");
                }
            }
            else if (parts.Length == 1)
            {
                if (BigInteger.TryParse(parts[0], out BigInteger result))
                {
                    return new BigRational(result);
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
                var bytes = Approximation(10, 8);
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

        public BigRational Pow(int Exponent)
        {
            return new BigRational(BigInteger.Pow(this.Numerator, Exponent), BigInteger.Pow(this.Denominator, Exponent));
        }

        public byte[] Approximation(byte Base, int precision)
        {
            if (Base < 2 || Base == 255) { throw new ArgumentException("Invalid base."); }

            BigInteger num = BigInteger.Abs(Numerator);
            byte[] approx;
            int index = 1;

            if (num >= Denominator)
            {
                BigInteger whole = num / Denominator;

                int exp = 0; BigInteger mul = 1;
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

            BigInteger dec = num % Denominator;
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
            if (ob is BigRational R)
            {
                if (this < R)
                {
                    return -1;
                }
                else if (this == R)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else if (ob is Rational r)
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

        public static explicit operator BigRational(long l)
        {
            return new BigRational(l);
        }

        public static explicit operator BigRational(Rational r)
        {
            return new BigRational(r.Numerator, r.Denominator);
        }

        /// <summary>
        /// The signum function
        /// </summary>
        public sbyte Sgn
        {
            get
            {
                if (this > 0) { return 1; }
                else if (this < 0) { return -1; }
                else { return 0; }
            }
        }

        public BigRational Inverse
        {
            get
            {
                return new BigRational(Denominator, Numerator);
            }
        }

        public BigRational Abs()
        {
            return (this < 0) ? -this : this;
        }
    }
}
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
using static RationalTypes.Utility;

namespace RationalTypes
{
    /// <summary>
    /// Represents a rational number.
    /// </summary>
    public partial struct Rational : IRational
    {
        #region UnaryOperators

        /// <summary>
        /// Negates the given number
        /// </summary>
        /// <param name="r">Given number</param>
        /// <returns></returns>
        public static Rational operator -(Rational r)
        {
            return (new Rational(-r._numerator, r._denominator));
        }

        /// <summary>
        /// Does nothing to the number.
        /// </summary>
        /// <param name="r">Given number</param>
        /// <returns>Argument r</returns>
        public static Rational operator +(Rational r)
        {
            return r;
        }

        #endregion UnaryOperators

        #region RationalOperatorOverloads

        public static Rational operator +(Rational p, Rational q)
        {
            long newDen = LCM(p.Denominator, q.Denominator);
            checked
            {
                return new Rational(p.Numerator * newDen / p.Denominator + q.Numerator * newDen / q.Denominator, newDen);
            }
        }

        public static Rational operator -(Rational p, Rational q)
        {
            long newDen = LCM(p.Denominator, q.Denominator);
            checked
            {
                return new Rational(p.Numerator * newDen / p.Denominator - q.Numerator * newDen / q.Denominator, newDen);
            }
        }

        public static Rational operator *(Rational p, Rational q)
        {
            long l = GDCE(p.Numerator, q.Denominator);
            long m = GDCE(p.Denominator, q.Numerator);
            checked
            {
                return new Rational((p.Numerator / l) * (q.Numerator / m), (p.Denominator / m) * (q.Denominator / l));
            }
        }

        public static Rational operator /(Rational p, Rational q)
        {
            return new Rational(p.Numerator * q.Denominator, p.Denominator * q.Numerator);
        }

        public static bool operator ==(Rational p, Rational q)
        {
            checked
            {
                return p.Numerator * q.Denominator == p.Denominator * q.Numerator;
            }
        }

        public static bool operator !=(Rational p, Rational q)
        {
            return !(p == q);
        }

        public static bool operator <(Rational p, Rational q)
        {
            long l = GDCE(p.Numerator, q.Numerator);
            long m = GDCE(p.Denominator, q.Denominator);
            checked
            {
                return (p.Numerator / l) * (q.Denominator / m) < (p.Denominator / m) * (q.Numerator / l);
            }
        }

        public static bool operator >(Rational p, Rational q)
        {
            long l = GDCE(p.Numerator, q.Numerator);
            long m = GDCE(p.Denominator, q.Denominator);
            checked
            {
                return (p.Numerator / l) * (q.Denominator / m) > (p.Denominator / m) * (q.Numerator / l);
            }
        }

        public static bool operator <=(Rational p, Rational q)
        {
            return !(p > q);
        }

        public static bool operator >=(Rational p, Rational q)
        {
            return !(p < q);
        }

        public static Rational operator %(Rational p, Rational q)
        {
            if (q < 0)
            {
                q = -q;
            }
            if (q == 0)
            {
                return (Rational)0;
            }
            checked
            {
                Rational res = p - q * (p / q).GetFloor;
                if (res < 0)
                {
                    res += q;
                }
                return res;
            }
        }

        #endregion RationalOperatorOverloads

        #region LongOperatorOverloads

        public static Rational operator +(Rational p, long q)
        {
            long newNum;
            checked
            {
                newNum = p.Numerator + q * p.Denominator;
            }
            return new Rational(newNum, p.Denominator);
        }

        public static Rational operator -(Rational p, long q)
        {
            long newNum;
            checked
            {
                newNum = p.Numerator - q * p.Denominator;
            }
            return new Rational(newNum, p.Denominator);
        }

        public static Rational operator *(Rational p, long q)
        {
            long div = GCD(p.Denominator, q);
            long newNum = p.Numerator;
            checked
            {
                newNum *= (q / div);
            }
            return new Rational(newNum, p.Denominator / div);
        }

        public static Rational operator *(long p, Rational q)
        {
            return q * p;
        }

        public static Rational operator /(Rational p, long q)
        {
            long div = GCD(p.Numerator, q);
            long newDen = p.Denominator;
            checked
            {
                newDen *= (q / div);
            }
            return new Rational(p.Numerator / div, newDen);
        }

        public static Rational operator /(long p, Rational q)
        {
            return p * q.Inverse;
        }

        public static bool operator ==(Rational p, long q)
        {
            return p.Numerator == q && p.Denominator == 1;
        }

        public static bool operator !=(Rational p, long q)
        {
            return !(p == q);
        }

        public static bool operator <(Rational p, long q)
        {
            return p.GetFloor < q;
        }

        public static bool operator >(Rational p, long q)
        {
            return p.GetCeiling > q;
        }

        public static bool operator <=(Rational p, long q)
        {
            return !(p > q);
        }

        public static bool operator >=(Rational p, long q)
        {
            return !(p < q);
        }

        public static Rational operator %(Rational p, long q)
        {
            q = Math.Abs(q);

            if (q == 0)
            {
                return (Rational)0;
            }

            Rational res = p - q * (p / q).GetFloor;
            if (res < 0)
            {
                res += q;
            }
            return res;
        }

        #endregion LongOperatorOverloads
    }
}
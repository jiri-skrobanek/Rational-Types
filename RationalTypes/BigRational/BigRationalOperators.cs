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

using System.Numerics;

namespace RationalTypes
{
    /// <summary>
    /// Represents rational number with high range of values.
    /// </summary>
    public partial struct BigRational : IRational
    {
        #region UnaryOperators

        /// <summary>
        /// Negates the number
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static BigRational operator -(BigRational r)
        {
            return new BigRational(-r._numerator, r._denominator);
        }

        /// <summary>
        /// Does nothing to the number
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static BigRational operator +(BigRational r)
        {
            return r;
        }

        #endregion UnaryOperators

        #region BigRationalOperatorOverloads

        public static BigRational operator +(BigRational p, BigRational q)
        {
            BigInteger newDen = Utility.BigLCM(p.Denominator, q.Denominator);
            return new BigRational(p.Numerator * newDen / p.Denominator + q.Numerator * newDen / q.Denominator, newDen);
        }

        public static BigRational operator -(BigRational p, BigRational q)
        {
            BigInteger newDen = Utility.BigLCM(p.Denominator, q.Denominator);
            return new BigRational(p.Numerator * newDen / p.Denominator - q.Numerator * newDen / q.Denominator, newDen);
        }

        public static BigRational operator *(BigRational p, BigRational q)
        {
            return new BigRational(p.Numerator * q.Numerator, p.Denominator * q.Denominator);
        }

        public static BigRational operator /(BigRational p, BigRational q)
        {
            return new BigRational(p.Numerator * q.Denominator, p.Numerator * q.Denominator);
        }

        public static bool operator ==(BigRational p, BigRational q)
        {
            return p.Numerator * q.Denominator == p.Denominator * q.Numerator;
        }

        public static bool operator !=(BigRational p, BigRational q)
        {
            return !(p == q);
        }

        public static bool operator <(BigRational p, BigRational q)
        {
            return p.Numerator * q.Denominator < p.Denominator * q.Numerator;
        }

        public static bool operator >(BigRational p, BigRational q)
        {
            return p.Numerator * q.Denominator > p.Denominator * q.Numerator;
        }

        public static bool operator <=(BigRational p, BigRational q)
        {
            return !(p > q);
        }

        public static bool operator >=(BigRational p, BigRational q)
        {
            return !(p < q);
        }

        #endregion BigRationalOperatorOverloads

        #region RationalOperatorOverloads

        public static BigRational operator +(BigRational p, Rational q)
        {
            BigInteger newDen = Utility.BigLCM(p.Denominator, q.Denominator);
            return new BigRational(p.Numerator * newDen / p.Denominator + q.Numerator * newDen / q.Denominator, newDen);
        }

        public static BigRational operator -(BigRational p, Rational q)
        {
            BigInteger newDen = Utility.BigLCM(p.Denominator, q.Denominator);
            return new BigRational(p.Numerator * newDen / p.Denominator - q.Numerator * newDen / q.Denominator, newDen);
        }

        public static BigRational operator *(BigRational p, Rational q)
        {
            return new BigRational(p.Numerator * q.Numerator, p.Denominator * q.Denominator);
        }

        public static BigRational operator *(Rational p, BigRational q)
        {
            return q * p;
        }

        public static BigRational operator /(BigRational p, Rational q)
        {
            return p * q.Inverse;
        }

        public static BigRational operator /(Rational p, BigRational q)
        {
            return p * q.Inverse;
        }

        public static bool operator ==(BigRational p, Rational q)
        {
            return p.Numerator * q.Denominator == p.Denominator * q.Numerator;
        }

        public static bool operator !=(BigRational p, Rational q)
        {
            return !(p == q);
        }

        public static bool operator <(BigRational p, Rational q)
        {
            return p.Numerator * q.Denominator < p.Denominator * q.Numerator;
        }

        public static bool operator >(BigRational p, Rational q)
        {
            return p.Numerator * q.Denominator > p.Denominator * q.Numerator;
        }

        public static bool operator <=(BigRational p, Rational q)
        {
            return !(p > q);
        }

        public static bool operator >=(BigRational p, Rational q)
        {
            return !(p < q);
        }

        #endregion RationalOperatorOverloads

        #region LongOperatorOverloads

        public static BigRational operator +(BigRational p, long q)
        {
            return new BigRational(p.Numerator + q * p.Denominator, p.Denominator);
        }

        public static BigRational operator -(BigRational p, long q)
        {
            return new BigRational(p.Numerator - q * p.Denominator, p.Denominator);
        }

        public static BigRational operator *(BigRational p, long q)
        {
            return new BigRational(p.Numerator * q, p.Denominator);
        }

        public static BigRational operator *(long p, BigRational q)
        {
            return q * p;
        }

        public static BigRational operator /(BigRational p, long q)
        {
            return new BigRational(p.Numerator, p.Denominator * q);
        }

        public static BigRational operator /(long p, BigRational q)
        {
            return p * q.Inverse;
        }

        public static bool operator ==(BigRational p, long q)
        {
            return p.Numerator == p.Denominator * q;
        }

        public static bool operator !=(BigRational p, long q)
        {
            return p.Numerator != p.Denominator * q;
        }

        public static bool operator <(BigRational p, long q)
        {
            return p.Numerator < p.Denominator * q;
        }

        public static bool operator >(BigRational p, long q)
        {
            return p.Numerator > p.Denominator * q;
        }

        public static bool operator <=(BigRational p, long q)
        {
            return !(p > q);
        }

        public static bool operator >=(BigRational p, long q)
        {
            return !(p < q);
        }

        #endregion LongOperatorOverloads
    }
}
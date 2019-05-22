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

namespace RationalTypes
{
    /// <summary>
    /// Polynomial with integral coefficients.
    /// </summary>
    public class Polynomial
    {
        /// <summary>
        /// The exponent of highest power of variable in polynomial
        /// </summary>
        public int Degree
        {
            get
            {
                return _coefficients.Length - 1;
            }
        }

        private long[] _coefficients;

        public long[] Coefficients
        {
            get
            {
                return _coefficients;
            }
        }

        public Polynomial(params long[] Coefficients)
        {
            int nonzero = Coefficients.Length - 1;
            for (int i = 0; i < Coefficients.Length; i++)
            {
                if (Coefficients[i] != 0)
                {
                    nonzero = i; break;
                }
            }

            long gcd = Utility.GCD(Coefficients);
            this._coefficients = new long[Coefficients.Length - nonzero];
            for (int i = nonzero; i < Coefficients.Length; i++)
            {
                this._coefficients[i - nonzero] = Coefficients[i] / gcd;
            }
        }

        public Polynomial(out long lcm, params Rational[] Coefficients)
        {
            this._coefficients = new long[Coefficients.Length];
            if (Coefficients.Length == 0)
            {
                throw new ArgumentException("Too few arguments.");
            }
            long[] denoms = new long[Coefficients.Length];
            int i = 0;
            foreach (var num in Coefficients)
            {
                denoms[i++] = num.Denominator;
            }
            lcm = Utility.LCM(denoms);
            i = 0;
            foreach (var num in Coefficients)
            {
                this._coefficients[i++] = num.Numerator * lcm / num.Denominator;
            }
        }

        /// <summary>
        /// Returns the value for certain value of variable.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public long At(long x)
        {
            long val = _coefficients[0];

            for (int i = 1; i <= Degree; i++)
            {
                val = x * val + _coefficients[i];
            }
            return val;
        }

        public long this[int index]
        {
            get
            {
                return _coefficients[index];
            }
        }

        public string ToString(char variable)
        {
            string res;
            if (Degree > 0)
            {
                if (Math.Abs(_coefficients[0]) == 1)
                {
                    res = (_coefficients[0] == 1 ? "" : "-") + variable.ToString() + "^" + Degree.ToString();
                }
                else
                {
                    res = _coefficients[0].ToString() + variable.ToString() + "^" + Degree.ToString();
                }
            }
            else
            {
                res = string.Empty;
            }
            for (int i = 1; i < Degree; i++)
            {
                if (_coefficients[i] == 0)
                {
                    continue;
                }
                else if (Math.Abs(_coefficients[i]) == 1)
                {
                    res += (_coefficients[i] == 1 ? "+" : "-") + variable.ToString() + "^" + (Degree - i).ToString();
                }
                else
                {
                    res += _coefficients[i].ToSignedString() + variable.ToString() + "^" + (Degree - i).ToString();
                }
            }
            res += Degree > 0 ? _coefficients[Degree].ToSignedString() : _coefficients[Degree].ToString();
            return res;
        }

        public override string ToString()
        {
            return ToString('x');
        }

        public Polynomial Derivative
        {
            get
            {
                var pol = new Polynomial
                {
                    _coefficients = new long[Degree]
                };
                for (int i = 0; i < Degree; i++)
                {
                    pol._coefficients[i] = _coefficients[i] * (Degree - i);
                }
                return pol;
            }
        }
    }
}
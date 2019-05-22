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

namespace RationalTypes
{
    /// <summary>
    /// Polynomial with rational coefficients.
    /// </summary>
    public class RationalPolynomial
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

        private Rational[] _coefficients;

        public Rational[] Coefficients
        {
            get
            {
                return _coefficients;
            }
        }

        public RationalPolynomial(params Rational[] Coefficients)
        {
            int nonzero = Coefficients.Length - 1;
            for (int i = 0; i < Coefficients.Length; i++)
            {
                if (Coefficients[i] != 0)
                {
                    nonzero = i; break;
                }
            }

            long gcd = Utility.GCD((from d in Coefficients select d.Denominator).ToArray());
            this._coefficients = new Rational[Coefficients.Length - nonzero];
            for (int i = nonzero; i < Coefficients.Length; i++)
            {
                this._coefficients[i - nonzero] = Coefficients[i] * gcd;
            }
        }

        /// <summary>
        /// Returns the value of polynomial for certain value of variable.
        /// </summary>
        /// <param name="x">Value of Variable</param>
        /// <returns></returns>
        public Rational At(Rational x)
        {
            Rational val = _coefficients[0];

            for (int i = 1; i <= Degree; i++)
            {
                val = x * val + _coefficients[i];
            }
            return val;
        }

        public Rational this[int index]
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
                if (_coefficients[0].Abs() == 1)
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
                else if (_coefficients[i].Abs() == 1)
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

        public string ToTeXString(char variable)
        {
            string res;
            if (Degree > 0)
            {
                if (_coefficients[0].Abs() == 1)
                {
                    res = (_coefficients[0] == 1 ? "" : "-") + variable.ToString() + "^{" + Degree.ToString() + "}";
                }
                else
                {
                    res = _coefficients[0].ToString() + variable.ToString() + "^{" + Degree.ToString() + " }";
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
                else if (_coefficients[i].Abs() == 1)
                {
                    res += (_coefficients[i] == 1 ? "+" : "-") + variable.ToString() + "^{" + (Degree - i).ToString() + "}";
                }
                else
                {
                    res += _coefficients[i].ToSignedTeXString() + variable.ToString() + "^{" + (Degree - i).ToString() + "}";
                }
            }
            res += Degree > 0 ? _coefficients[Degree].ToSignedTeXString() : _coefficients[Degree].ToTeXString();
            return res;
        }

        public RationalPolynomial Derivative
        {
            get
            {
                var pol = new RationalPolynomial
                {
                    _coefficients = new Rational[Degree]
                };
                for (int i = 0; i < Degree; i++)
                {
                    pol._coefficients[i] = _coefficients[i] * (Degree - i);
                }
                return pol;
            }
        }

        public Polynomial MakePolynomial()
        {
            var lcm = Utility.LCM(_coefficients.Select(x => x.Denominator).ToArray());

            return new Polynomial(_coefficients.Select(x => x.Numerator * (lcm / x.Denominator)).ToArray());
        }

        /// <summary>
        /// Divides the polynomial by (x - solution).
        /// </summary>
        /// <param name="solution"></param>
        /// <returns>Whether division succeeded</returns>
        public bool DivideBySolution(Rational solution)
        {
            try
            {
                var newcoef = new Rational[Degree];
                Rational carry = _coefficients[0];

                newcoef[0] = _coefficients[0];
                for (int i = 1; i < Degree; i++)
                {
                    carry = newcoef[i] = _coefficients[i] + carry * solution;
                }

                _coefficients = newcoef;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
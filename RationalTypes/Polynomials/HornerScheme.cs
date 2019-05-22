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

namespace RationalTypes
{
    public static class HornerScheme
    {
        public static Rational[] GetRationalRoots(Polynomial p)
        {
            if (p.Degree == 0)
            {
                return new Rational[0];
            }
            if (p.Degree == 1)
            {
                return new Rational[] { new Rational(-p[1], p[0]) };
            }

            HashSet<Rational> Roots = new HashSet<Rational>();

            if (p[p.Degree] == 0)
            {
                long[] coef = new long[p.Degree];
                for (int i = 0; i < p.Degree; i++)
                {
                    coef[i] = p[i];
                }
                Polynomial q = new Polynomial(coef);
                Roots.UnionWith(GetRationalRoots(q));
                if (!Roots.Contains((Rational)0))
                {
                    Roots.Add((Rational)0);
                }
                var arri = new Rational[Roots.Count];
                Roots.CopyTo(arri);
                return arri;
            }

            long cnsqrt = Utility.IntegralPartSquareRoot(Math.Abs(p[p.Degree]));
            long c1sqrt = Utility.IntegralPartSquareRoot(Math.Abs(p[0]));

            for (int i = 1; i <= cnsqrt; i++)
            {
                if (p[p.Degree] % i == 0)
                {
                    // i:
                    BranchingI(i);

                    // c_n / i:
                    BranchingI(p[p.Degree] / i);
                }
            }

            /*Roots.Sort();

            for (int i = 1; i < Roots.Count;)
            {
                if (Roots[i] == Roots[i - 1])
                {
                    Roots.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
            */
            var arr = new Rational[Roots.Count];
            Roots.CopyTo(arr);
            return arr;

            void BranchingI(long i)
            {
                for (long j = 1; j <= c1sqrt; j++)
                {
                    if (Utility.GCD(i, j) > 1)
                    {
                        continue;
                    }

                    if (p[0] % j == 0)
                    {
                        // j
                        BranchingJ(j);
                        // c_1 / j
                        BranchingJ(p[0] / j);
                    }
                }

                void BranchingJ(long j)
                {
                    Rational sum = new Rational(p[0]);
                    Rational q = new Rational(i, j);
                    int k = 1;
                    while (k <= p.Degree)
                    {
                        sum *= q;
                        sum += p[k];
                        k++;
                    }
                    if (sum.Numerator == 0)
                    {
                        Roots.Add(q);
                    }

                    sum = new Rational(p[0]);
                    k = 1;
                    while (k <= p.Degree)
                    {
                        sum *= -q;
                        sum += p[k];
                        k++;
                    }
                    if (sum.Numerator == 0)
                    {
                        Roots.Add(-q);
                    }
                }
            }
        }

        public static string GetFactorForm(RationalPolynomial p)
        {
            var q = new RationalPolynomial(p.Coefficients);
            string answer = string.Empty;
            var roots = GetRationalRoots(q.MakePolynomial());
            foreach (var root in roots)
            {
                while (q.At(root) == 0)
                {
                    answer += "(x" + (-root).ToSignedString() + ")";
                    q.DivideBySolution(root);
                }
            }

            answer += (q.Degree == 0 && q[0] == 1) ? string.Empty : "(" + q.ToString() + ")";

            return answer;
        }

        public static string GetTeXFactorForm(RationalPolynomial p)
        {
            var q = new RationalPolynomial(p.Coefficients);
            string answer = string.Empty;
            var roots = GetRationalRoots(q.MakePolynomial());
            foreach (var root in roots)
            {
                while (q.At(root) == 0)
                {
                    answer += "\\left( x" + (-root).ToSignedTeXString() + "\\right)";
                    q.DivideBySolution(root);
                }
            }

            answer += (q.Degree == 0 && q[0] == 1) ? string.Empty : "\\left( " + q.ToString() + "\\right)";

            return answer;
        }
    }
}
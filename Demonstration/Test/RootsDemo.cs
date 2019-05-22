using RationalTypes;
using System;

namespace Demonstration
{
    internal class RootsDemo
    {
        public static void Solutions()
        {
            Next(new RationalPolynomial(new Rational[]
            {
                -(Rational)45632
            }
            ));

            Next(new RationalPolynomial(new Rational[]
            {
                -(Rational)28,
                (Rational)10
            }
            ));

            Next(new RationalPolynomial(new Rational[]
            {
                (Rational) 1/2,
                (Rational) 1,
                (Rational) 0,
            }
            ));

            Next(new RationalPolynomial(new Rational[]
            {
                (Rational) 1/2,
                -(Rational) 1,
                (Rational) 2,
            }
            ));

            Next(new RationalPolynomial(new Rational[]
            {
                (Rational) 56,
                (Rational) 616/10,
                -(Rational) 38976/100,
                (Rational)28224/100
            }
            ));

            Next(new RationalPolynomial(new Rational[]
            {
                (Rational) 1,
                -(Rational) 12/7,
                (Rational) 54/49,
                -(Rational)108/343,
                (Rational)81/2401
            }
            ));

            return;

            void Next(RationalPolynomial rp)
            {
                Console.WriteLine(rp.ToString('x'));
                foreach (var r in HornerScheme.GetRationalRoots(rp.MakePolynomial()))
                {
                    Console.Write(r.ToString() + "\t");
                }
                Console.Write("\n");
            }
        }

        public static void FactorForms()
        {
            var pol = new RationalTypes.RationalPolynomial(new RationalTypes.Rational[]
            {
                new RationalTypes.Rational(17),
                new RationalTypes.Rational(187),
                new RationalTypes.Rational(612),
                new RationalTypes.Rational(272),
                new RationalTypes.Rational(-1088)
            });
            Console.WriteLine(RationalTypes.HornerScheme.GetFactorForm(pol));

            pol = new RationalTypes.RationalPolynomial(new RationalTypes.Rational[]
            {
                new RationalTypes.Rational(1),
                new RationalTypes.Rational(-2),
                new RationalTypes.Rational(1),
                new RationalTypes.Rational(-18),
                new RationalTypes.Rational(-72)
            });
            Console.WriteLine(RationalTypes.HornerScheme.GetFactorForm(pol));

            pol = new RationalTypes.RationalPolynomial(new RationalTypes.Rational[]
            {
                new RationalTypes.Rational(12),
                new RationalTypes.Rational(2496,5),
                new RationalTypes.Rational(-1826),
                new RationalTypes.Rational(416),
                new RationalTypes.Rational(-1528),
                new RationalTypes.Rational(416,5),
                new RationalTypes.Rational(-306)

                // -306 + (416 x) / 5 - 1528 x ^ 2 + 416 x ^ 3 - 1826 x ^ 4 + (2496 x ^ 5)/ 5 + 12 x ^ 6
            });
            Console.WriteLine(RationalTypes.HornerScheme.GetFactorForm(pol));

            pol = new RationalPolynomial(new Rational[]
            {
                (Rational)1,
                -(Rational)12 / 7,
                (Rational)54 / 49,
                -(Rational)108 / 343,
                (Rational)81 / 2401
            });
            Console.WriteLine(RationalTypes.HornerScheme.GetFactorForm(pol));
        }
    }
}
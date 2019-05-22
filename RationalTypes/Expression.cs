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
using System.Text;

namespace RationalTypes
{
    /// <summary>
    /// Class for evluating mathematical strings.
    /// </summary>
    public class Expression
    {
        private enum ElType
        {
            Sign, Numeric, Div, Mul
        }

        private class Element
        {
            public ElType Type;
            public dynamic value;
        }

        public static Rational Evaluate(string expression)
        {
            if (expression.Length == 0)
            {
                throw new Exception("No content.");
            }
            if (expression[0] == '*' || expression[0] == '/')
            {
                throw new Exception("Invalid beginning.");
            }
            if (expression[expression.Length - 1] == '*' ||
                expression[expression.Length - 1] == '/' ||
                expression[expression.Length - 1] == '-' ||
                expression[expression.Length - 1] == '+')
            {
                throw new Exception("Invalid ending.");
            }

            bool wasNumber = false;
            List<Element> elements = new List<Element>();
            for (int i = 0; i < expression.Length;)
            {
                switch (expression[i])
                {
                    case '(':
                        {
                            i++;
                            StringBuilder builder = new StringBuilder();
                            int indent = 1;
                            while (i < expression.Length)
                            {
                                if (expression[i] == ')') { indent--; if (indent == 0) { break; } }
                                else if (expression[i] == '(')
                                {
                                    indent++;
                                }
                                builder.Append(expression[i++]);
                            }
                            i++;
                            elements.Add(new Element { Type = ElType.Numeric, value = Evaluate(builder.ToString()) });
                            wasNumber = false;
                            break;
                        }
                    case '+':
                    case '-':
                        elements.Add(new Element { Type = ElType.Sign, value = expression[i] });
                        i++;
                        wasNumber = false;
                        break;

                    case '*':
                        i++; wasNumber = false;
                        break;

                    case '/':
                        elements.Add(new Element { Type = ElType.Div });
                        i++;
                        wasNumber = false;
                        break;

                    case ' ':
                        i++; break;
                    default:
                        {
                            StringBuilder builder = new StringBuilder();
                            while (i < expression.Length &&
                                expression[i] != '+' &&
                                expression[i] != '-' &&
                                expression[i] != '*' &&
                                expression[i] != '/' &&
                                expression[i] != '(' &&
                                expression[i] != ')' &&
                                expression[i] != ' ')
                            {
                                builder.Append(expression[i++]);
                            }
                            if (wasNumber)
                            {
                                throw new Exception("Repeating numerics.");
                            }
                            elements.Add(new Element { Type = ElType.Numeric, value = Rational.Parse(builder.ToString()) });
                            wasNumber = true;
                            break;
                        }
                }
            }

            Rational value = (Rational)0;
            Rational mulvalue = (Rational)0;
            SByte sign = 1;

            for (int i = 0; i < elements.Count;)
            {
                switch (elements[i].Type)
                {
                    case ElType.Sign:
                        value += mulvalue;
                        mulvalue = (Rational)0;
                        if (sign != 0 && i > 0)
                        {
                            throw new Exception("Repeating signs.");
                        }
                        if ((char)elements[i].value == '-') { sign = -1; }
                        else if ((char)elements[i].value == '+') { sign = 1; }
                        i++;
                        break;

                    case ElType.Numeric:
                        mulvalue += (Rational)elements[i].value * sign;
                        if (sign == 0) { mulvalue *= (Rational)elements[i].value; }
                        sign = 0;
                        i++;
                        break;

                    case ElType.Mul:
                        mulvalue *= (Rational)elements[i + 1].value;
                        i += 2;
                        break;

                    case ElType.Div:
                        if (elements[i + 1].Type != ElType.Numeric)
                        {
                            throw new Exception("Repeating operators.");
                        }
                        mulvalue /= (Rational)elements[i + 1].value;
                        i += 2;
                        break;
                }
            }

            value += mulvalue;

            return value;
        }
    }
}
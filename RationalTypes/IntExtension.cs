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
    /// Provides extension methods to the sealed numeric structs.
    /// </summary>
    internal static class IntExtension
    {
        public static string ToSignedString(this long i)
        {
            if (i < 0)
            {
                return i.ToString();
            }
            else
            {
                return "+" + i.ToString();
            }
        }

        public static string ToSignedString(this BigInteger i)
        {
            if (i < 0)
            {
                return i.ToString();
            }
            else
            {
                return "+" + i.ToString();
            }
        }
    }
}
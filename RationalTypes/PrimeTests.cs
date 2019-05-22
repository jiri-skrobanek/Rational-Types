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
    public static class PrimeTests
    {
        public static bool FermatTheorem(long l)
        {
            long[] mods = new long[64]; BigInteger val = 2;
            for (int i = 1; i < 64; i++)
            {
                mods[i] = (long)val;
                val = (val * val) % l;
            }

            BigInteger sum = 1; long pow = 1;
            for (int i = 0; i < 63; i++)
            {
                if ((l & pow) > 0)
                {
                    sum = sum * mods[i + 1] % l;
                }
                pow *= 2;
            }
            if (sum == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
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
    /// Overlaying type for Rational numbers.
    /// Not to be confused with irrational type.
    /// </summary>
    internal interface IRational : IComparable
    {
        #region Rounding

        void Round();

        void Truncate();

        void Ceiling();

        void Floor();

        void Stretch();

        #endregion Rounding

        string DecimalApproximation { get; }

        /// <summary>
        /// Gives approximate double value.
        /// </summary>
        double DoublePrecisionValue { get; }

        /// <summary>
        /// Gives approximate single value.
        /// </summary>
        float SinglePrecisionValue { get; }

        /// <summary>
        /// Gives TEX notation of this <see cref="RationalTypes.IRational"/>
        /// </summary>
        /// <returns>TeX representation of this number</returns>
        string ToTeXString();

        /// <summary>
        /// Converts this <see cref="RationalTypes.IRational"/> to corresponding string representation with sign.
        /// </summary>
        string ToSignedString();

        /// <summary>
        /// Converts this <see cref="RationalTypes.IRational"/> to corresponding string representation with sign.
        /// </summary>
        string ToSignedTeXString();

        byte[] Approximation(byte Base, int precision);
    }
}
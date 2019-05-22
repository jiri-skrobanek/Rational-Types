﻿/********************************************************************
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
    public partial struct BigRational : IRational
    {
        #region Properties

        public BigInteger GetFloor
        {
            get
            {
                if (_numerator < 0)
                {
                    return (_numerator + 1) / _denominator - 1;
                }
                else
                {
                    return _numerator / _denominator;
                }
            }
        }

        public BigInteger GetCeiling
        {
            get
            {
                if (_numerator > 0)
                {
                    return (_numerator - 1) / _denominator + 1;
                }
                else
                {
                    return _numerator / _denominator;
                }
            }
        }

        public BigInteger GetTruncate
        {
            get
            {
                return _numerator / _denominator;
            }
        }

        /// <summary>
        /// Will round arithmetically.
        /// </summary>
        public BigInteger GetRound
        {
            get
            {
                var cmp = (_numerator * Sgn) % _denominator;
                if (cmp > _denominator / 2)
                {
                    return GetStretch;
                }
                else if (cmp * 2 == _denominator)
                {
                    return GetCeiling;
                }
                else
                {
                    return GetTruncate;
                }
            }
        }

        public BigInteger
            GetStretch
        {
            get
            {
                if (_numerator >= 0)
                {
                    return GetCeiling;
                }
                else
                {
                    return GetFloor;
                }
            }
        }

        #endregion Properties

        #region Methods

        public void Floor()
        {
            if (_numerator < 0)
            {
                _numerator = (_numerator + 1) / _denominator - 1;
            }
            else
            {
                _numerator = _numerator / _denominator;
            }

            _denominator = 1;
        }

        public void Ceiling()
        {
            if (_numerator > 0)
            {
                _numerator = (_numerator - 1) / _denominator + 1;
            }
            else
            {
                _numerator = _numerator / _denominator;
            }
            _denominator = 1;
        }

        public void Truncate()
        {
            _numerator = _numerator / _denominator;
            _denominator = 1;
        }

        public void Stretch()
        {
            if (_numerator >= 0)
            {
                Ceiling();
            }
            else
            {
                Floor();
            }
        }

        /// <summary>
        /// Will round arithmetically.
        /// </summary>
        public void Round()
        {
            var cmp = (_numerator * Sgn) % _denominator;
            if (cmp > _denominator / 2)
            {
                Stretch();
            }
            else if (cmp * 2 == _denominator)
            {
                Ceiling();
            }
            else
            {
                Truncate();
            }
        }

        #endregion Methods
    }
}
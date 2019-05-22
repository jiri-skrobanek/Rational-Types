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

namespace RationalTypes.Exceptions
{
    /// <summary>
    /// Indicates that the result of an operation is not a rational number.
    /// </summary>
    public class IrrationalResultException : Exception
    {
        private string _message;

        public override string Message
        {
            get
            {
                return _message;
            }
        }

        public IrrationalResultException() : this("The result of this operation is not rational number.")
        { }

        public IrrationalResultException(string Message)
        {
            this._message = Message;
        }
    }
}
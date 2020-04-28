// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Channels.Networking.Libuv.Interop
{
    public class UvException : Exception
    {
        public UvException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; }
    }
}

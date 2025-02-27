﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Runtime.InteropServices.Marshalling
{
    public interface IComObjectWrapper<T> { }

    [AttributeUsage(AttributeTargets.Interface)]
    public class GeneratedComInterfaceAttribute : Attribute
    {
    }
}

﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

namespace Unity.CoreCLRHelpers;

[AttributeUsage(AttributeTargets.Method)]
class NativeFunctionAttribute : Attribute
{
    public NativeFunctionAttribute(NativeFunctionOptions options)
    {
    }

    public NativeFunctionAttribute(string name)
    {
    }

    public NativeFunctionAttribute(string name, NativeFunctionOptions options)
    {
    }
}

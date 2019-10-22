﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZTunnel.Pmms.Core.Attributes
{
    /// <summary>
    /// Optional Key attribute.
    /// You can use the System.ComponentModel.DataAnnotations version in its place to specify a required property of a poco
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredAttribute : Attribute
    {
    }
}

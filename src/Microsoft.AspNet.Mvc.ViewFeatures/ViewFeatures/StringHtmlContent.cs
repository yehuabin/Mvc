﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Diagnostics;
using System.IO;
using Microsoft.AspNet.Html.Abstractions;
using Microsoft.Framework.Internal;
using Microsoft.Framework.WebEncoders;

namespace Microsoft.AspNet.Mvc.ViewFeatures
{
    /// <summary>
    /// String content which gets encoded when written.
    /// </summary>
    [DebuggerDisplay("{DebuggerToString()}")]
    public class StringHtmlContent : IHtmlContent
    {
        private readonly string _input;

        /// <summary>
        /// Creates a new instance of <see cref="StringHtmlContent"/>
        /// </summary>
        /// <param name="input"><see cref="string"/> to be HTML encoded when <see cref="WriteTo"/> is called.</param>
        public StringHtmlContent(string input)
        {
            _input = input;
        }

        /// <inheritdoc />
        public void WriteTo([NotNull] TextWriter writer, [NotNull] IHtmlEncoder encoder)
        {
            encoder.HtmlEncode(_input, writer);
        }

        private string DebuggerToString()
        {
            using (var writer = new StringWriter())
            {
                WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }
    }
}
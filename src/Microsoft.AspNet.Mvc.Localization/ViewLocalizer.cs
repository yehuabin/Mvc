// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Globalization;
using Microsoft.Framework.Internal;
using Microsoft.Framework.Localization;
using Microsoft.Dnx.Runtime;
using Microsoft.AspNet.Mvc.ViewFeatures;

namespace Microsoft.AspNet.Mvc.Localization
{
    /// <summary>
    /// A <see cref="HtmlLocalizer"/> that provides localized strings for views.
    /// </summary>
    public class ViewLocalizer : IViewLocalizer
    {
        private readonly IHtmlLocalizerFactory _localizerFactory;
        private readonly string _applicationName;
        private IHtmlLocalizer _localizer;

        /// <summary>
        /// Creates a new <see cref="ViewLocalizer"/>.
        /// </summary>
        /// <param name="localizerFactory">The <see cref="IHtmlLocalizerFactory"/>.</param>
        /// <param name="applicationEnvironment">The <see cref="IApplicationEnvironment"/>.</param>
        public ViewLocalizer(
            [NotNull] IHtmlLocalizerFactory localizerFactory,
            [NotNull] IApplicationEnvironment applicationEnvironment,
            [NotNull] IViewContextAccessor viewContextAccessor)
        {
            _applicationName = applicationEnvironment.ApplicationName;
            _localizerFactory = localizerFactory;

            var viewContext = viewContextAccessor.CurrentContext;
            var baseName = viewContext.View.Path.Replace('/', '.').Replace('\\', '.');
            if (baseName.StartsWith("."))
            {
                baseName = baseName.Substring(1);
            }
            baseName = _applicationName + "." + baseName;
            _localizer = _localizerFactory.Create(baseName, _applicationName);
        }

        /// <inheritdoc />
        public LocalizedString this[[NotNull] string name] => _localizer[name];

        /// <inheritdoc />
        public LocalizedString this[[NotNull] string name, params object[] arguments] => _localizer[name, arguments];

        /// <inheritdoc />
        public LocalizedString GetString([NotNull] string name) => _localizer.GetString(name);

        /// <inheritdoc />
        public LocalizedString GetString([NotNull] string name, params object[] values) =>
            _localizer.GetString(name, values);

        /// <inheritdoc />
        public LocalizedHtmlString Html([NotNull] string key) => _localizer.Html(key);

        /// <inheritdoc />
        public LocalizedHtmlString Html([NotNull] string key, params object[] arguments) =>
            _localizer.Html(key, arguments);

        /// <inheritdoc />
        public IStringLocalizer WithCulture([NotNull] CultureInfo culture) => _localizer.WithCulture(culture);

        /// <inheritdoc />
        IHtmlLocalizer IHtmlLocalizer.WithCulture([NotNull] CultureInfo culture) => _localizer.WithCulture(culture);

        /// <inheritdoc />
        public IEnumerable<LocalizedString> GetAllStrings(bool includeAncestorCultures) =>
            _localizer.GetAllStrings(includeAncestorCultures);
    }
}
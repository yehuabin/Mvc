// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Framework.Internal;

namespace Microsoft.AspNet.Mvc
{
    public struct ViewContextState : IDisposable
    {
        private readonly IViewContextAccessor _accessor;

        public ViewContextState([NotNull] IViewContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public void Dispose()
        {
            _accessor?.PopContext();
        }
    }
}

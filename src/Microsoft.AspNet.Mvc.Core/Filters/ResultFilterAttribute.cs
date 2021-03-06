// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Microsoft.Framework.Internal;

namespace Microsoft.AspNet.Mvc.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class ResultFilterAttribute : Attribute, IResultFilter, IAsyncResultFilter, IOrderedFilter
    {
        public int Order { get; set; }

        public virtual void OnResultExecuting([NotNull] ResultExecutingContext context)
        {
        }

        public virtual void OnResultExecuted([NotNull] ResultExecutedContext context)
        {
        }

        public virtual async Task OnResultExecutionAsync(
            [NotNull] ResultExecutingContext context,
            [NotNull] ResultExecutionDelegate next)
        {
            OnResultExecuting(context);
            if (!context.Cancel)
            {
                OnResultExecuted(await next());
            }
        }
    }
}

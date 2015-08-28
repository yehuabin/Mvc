// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
#if DNX451
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
#else
using System.Threading;
#endif

namespace Microsoft.AspNet.Mvc
{
    public class ViewContextAccessor : IViewContextAccessor
    {
#if DNX451
        private static string Key = typeof(ViewContext).FullName;

        protected Stack<ViewContext> Storage
        {
            get
            {
                Stack<ViewContext> storage;
                var handle = CallContext.LogicalGetData(Key) as ObjectHandle;
                if (handle == null)
                {
                    storage = new Stack<ViewContext>();
                    CallContext.LogicalSetData(Key, new ObjectHandle(storage));
                }
                else
                {
                    storage = (Stack<ViewContext>)handle.Unwrap();
                }

                return storage;
            }
        }
#else
        private readonly AsyncLocal<Stack<ViewContext>> _storage = new AsyncLocal<Stack<ViewContext>>();

        public Stack<ViewContext> Storage
        {
            get
            { 
                if (_storage.Value == null)
                {
                    _storage.Value = new Stack<ViewContext>();
                }

                return _storage.Value; 
            }
        }
#endif

        public ViewContext CurrentContext
        {
            get
            {
                var storage = Storage;
                if (storage.Count == 0)
                {
                    return null;
                }
                else
                {
                    return storage.Peek();
                }
            }
        }

        public ViewContext PopContext()
        {
            return Storage.Pop();
        }

        public ViewContextState PushContext(ViewContext context)
        {
            Storage.Push(context);
            return new ViewContextState(this);
        }
    }
}

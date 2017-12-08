using System;
using System.Collections.Generic;

namespace Station.Common.Contracts
{
    public interface IDependencyResolver : IDisposable
    {
        IDependencyResolver CreateScope();
        T Resolve<T>();
        IEnumerable<T> ResolveAll<T>();
        T TryResolve<T>();
    }
}

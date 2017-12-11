using Station.Common.Interfaces;
using System;

namespace Station.Common.Contracts
{
    public interface ICommonUnitOfWork : IDisposable
    {
        T GetRepository<T>() where T : IRepository;
    }
}

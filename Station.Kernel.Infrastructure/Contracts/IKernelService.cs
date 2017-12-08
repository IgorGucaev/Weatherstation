using System;
using System.Collections.Generic;
using System.Text;

namespace Station.Kernel.Infrastructure.Contracts
{
    public interface IKernelService<TUnitOfWork>
        where TUnitOfWork : IKernelUnitOfWork
    {
        TUnitOfWork UnitOfWork { get; }
    }
}

using Station.Common.Classes;
using Station.Kernel.Infrastructure;
using Station.Modules.Signals.Domain.Contracts;
using Station.Modules.Signals.Infrastructure.Data;

namespace Station.Modules.Signals.Infrastructure
{
    [DBAttribute(Common.Enums.DBType.Sql)]
    public class SignalUnitOfWork : KernelContextUnitOfWork<SignalDbContext>, IStationUnitOfWork
    {
        protected SignalDbContext _lversion;

        public SignalUnitOfWork() : base() { }
    }
}

using Station.Common.Contracts;
using Station.Modules.Signals.Domain.Entities;

namespace Station.Modules.Signals.Domain.Contracts.Repositories
{
    public interface ISignalRepository : ICommonRepository<Signal, long>
    {
        Signal Get(long Id);
    }
}
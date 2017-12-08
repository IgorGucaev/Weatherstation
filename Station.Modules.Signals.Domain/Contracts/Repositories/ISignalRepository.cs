using Station.Common.Contracts;
using Station.Modules.Signals.Domain.Entities;
using System.Collections.Generic;

namespace Station.Modules.Signals.Domain.Contracts.Repositories
{
    public interface ISignalRepository : ICommonRepository<Signal, long>
    {
        Signal GetByID(long SignalId);
        List<Signal> GetByContractId(long contractId);
    }
}
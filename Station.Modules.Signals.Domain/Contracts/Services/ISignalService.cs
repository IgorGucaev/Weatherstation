using Station.Modules.Signals.Domain.Entities;
using System.Collections.Generic;

namespace Station.Modules.Signals.Domain.Contracts.Services
{
    public interface ISignalService
    {
        Signal Get(long id);

        List<Signal> GetByContract(long contractId);
    }
}

using System.Linq;
using System.Collections.Generic;
using Station.Kernel.Infrastructure.Repositories;
using Station.Modules.Signals.Domain.Contracts.Repositories;
using Station.Modules.Signals.Domain.Entities;
using Station.Modules.Signals.Infrastructure.Data;

namespace Station.Modules.Signals.Infrastructure.Repositories
{
    public class SignalRepository : BaseRepository<StationDbContext, Signal, long>, ISignalRepository
    {
        public SignalRepository(StationDbContext context) : base(context) { }

        public List<Signal> GetByContractId(long contractId)
        {
            return base.GetAll().Where(d => d.ContractID == contractId).ToList();
        }

        public Signal GetByID(long SignalId)
        {
            return base.Get(SignalId);
        }
    }
}

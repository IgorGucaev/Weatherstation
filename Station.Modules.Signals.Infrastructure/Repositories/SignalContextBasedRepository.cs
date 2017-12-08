using Station.Kernel.Infrastructure.Repositories;
using Station.Modules.Signals.Domain.Contracts.Repositories;
using Station.Modules.Signals.Domain.Entities;
using Station.Modules.Signals.Infrastructure.Data;

namespace Station.Modules.Signals.Infrastructure.Repositories
{
    public class SignalContextBasedRepository : BaseRepository<StationDbContext, Signal, long>, ISignalRepository
    {
        public SignalContextBasedRepository(StationDbContext context) : base(context) { }

        public Signal GetByID(long SignalId)
        {
            return base.Get(SignalId);
        }
    }
}

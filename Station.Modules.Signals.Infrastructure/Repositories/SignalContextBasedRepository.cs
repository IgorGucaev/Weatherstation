using Station.Common.Classes;
using Station.Kernel.Infrastructure.Repositories;
using Station.Modules.Signals.Domain.Contracts.Repositories;
using Station.Modules.Signals.Domain.Entities;
using Station.Modules.Signals.Infrastructure.Data;

namespace Station.Modules.Signals.Infrastructure.Repositories
{
    [DBAttribute(Common.Enums.DBType.Sql)]
    public class SignalContextBasedRepository : BaseRepository<SignalDbContext, Signal, long>, ISignalRepository
    {
        public SignalContextBasedRepository(SignalDbContext context) : base(context)
        { }

        public Signal GetByID(long SignalId)
        {
            return base.Get(SignalId);
        }
    }
}

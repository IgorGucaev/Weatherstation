using Station.Modules.Signals.Domain.Contracts.Repositories;
using Station.Modules.Signals.Domain.Entities;
using Station.Modules.Signals.Infrastructure.Data.Configurations.Bson;
using Station.Common.Infrastructure.Repositories;
using Station.Modules.Signals.Infrastructure.Data;
using Station.Common.Classes;

namespace Station.Modules.Signals.Infrastructure.Repositories
{
    [DBAttribute(Common.Enums.DBType.NoSql)]
    public class SignalMongoBasedRepository : CommonMongoBasedRepository<SignalMongoDB, Signal, long>, ISignalRepository
    {
        public SignalMongoBasedRepository(SignalMongoDB client) : base(client)
        {
            var bson = new SignalBsonConfiguration();
            bson.Configure();
        }
        public Signal GetByID(long SignalId)
        {
            return base.Get(SignalId);
        }

        public Signal GetTracking(long id)
        {
            throw new System.NotImplementedException("");
        }
    }
}

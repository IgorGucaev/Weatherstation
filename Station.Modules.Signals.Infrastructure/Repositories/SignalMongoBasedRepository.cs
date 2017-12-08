using Station.Modules.Signals.Domain.Contracts.Repositories;
using Station.Modules.Signals.Domain.Entities;
using Station.Modules.Signals.Infrastructure.Data.Configurations.Bson;
using Station.Common.Infrastructure.Repositories;
using MongoDB.Driver;

namespace Station.Modules.Signals.Infrastructure.Repositories
{
    public class SignalMongoBasedRepository : CommonMongoBasedRepository<IMongoDatabase, Signal, long>, ISignalRepository
    {
        public SignalMongoBasedRepository(IMongoDatabase database, string collectionName) : base(database, collectionName)
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

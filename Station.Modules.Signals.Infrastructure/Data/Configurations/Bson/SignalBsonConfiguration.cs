using MongoDB.Bson.Serialization;
using Station.Kernel.Infrastructure.Data;
using Station.Modules.Signals.Domain.Entities;

namespace Station.Modules.Signals.Infrastructure.Data.Configurations.Bson
{
    public class SignalBsonConfiguration : BsonConfiguration<Signal>
    {
        public override void Configure()
        {
            BsonClassMap.RegisterClassMap<Signal>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(p => p.Value).SetElementName("value");
            });
        }
    }
}

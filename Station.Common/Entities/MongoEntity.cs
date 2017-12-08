using MongoDB.Bson;

namespace Station.Common.Entities
{
    abstract class MongoEntity
    {
        public ObjectId Id { get; set; }
    }
}

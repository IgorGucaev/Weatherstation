using MongoDB.Driver;

namespace Station.Kernel.Infrastructure.Data
{
    public class KernelMongoClient : MongoClient
    {
        public KernelMongoClient()
            :base()
        { }

        public KernelMongoClient(string connectionString)
            : base(connectionString)
        {  }
    }
}
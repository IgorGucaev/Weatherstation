using Station.Common.Classes;
using Station.Kernel.Infrastructure.Data;

namespace Station.Modules.Signals.Infrastructure.Data
{
    public class SignalMongoDB : KernelMongoClient
    {
        public SignalMongoDB()
            : base(AppConfiguration.Configuration["SignalFromNoSql"])
        { }
    }
}

using Station.Kernel.Infrastructure;
using Station.Modules.Signals.Domain.Contracts;
using Station.Modules.Signals.Infrastructure.Data;

namespace Station.Modules.Signals.Infrastructure
{
    public class StationMongoUnitOfWork : KernelMongoUnitOfWork<StationMongoDB>, IStationUnitOfWork
    {
        protected StationDbContext _lversion;

        public StationMongoUnitOfWork() : base() { }

        //protected LVDbContext LightVersion
        //{
        //    get
        //    {
        //        if (_lversion == null)
        //        {
        //            var optionsBuilder = new DbContextOptionsBuilder<CommonDbContext>();
        //            optionsBuilder.UseSqlServer("Data Source=5.178.84.50;Initial Catalog=InvoiceVerificationTest;User ID=rply;Password=ini9qwevbn.");
        //            _lversion = new LVDbContext(optionsBuilder.Options);
        //        }

        //        return _lversion;
        //    }
        //}
    }
}

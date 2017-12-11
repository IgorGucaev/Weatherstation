using Station.Common.Classes;
using Station.Kernel.Infrastructure;
using Station.Modules.Signals.Domain.Contracts;
using Station.Modules.Signals.Infrastructure.Data;

namespace Station.Modules.Signals.Infrastructure
{
    [DBAttribute(Common.Enums.DBType.NoSql)]
    public class SignalMongoUnitOfWork : KernelMongoUnitOfWork<SignalMongoDB>, IStationUnitOfWork
    {
        protected SignalDbContext _lversion;

        [DBAttribute(Common.Enums.DBType.NoSql)]
        public SignalMongoUnitOfWork() : base() { }

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

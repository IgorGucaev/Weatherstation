using Station.Modules.Signals.Domain.Contracts;
using Station.Modules.Signals.Domain.Contracts.Repositories;
using Station.Modules.Signals.Domain.Contracts.Services;
using Station.Modules.Signals.Domain.Entities;

namespace Station.Modules.Signals.Domain.Services
{
    public class SignalService : MainService, ISignalService
    {
        public const int TRANSACTION_MINUTES = 5;

        protected ISignalRepository SignalRepository
        { get { return this.UnitOfWork?.GetRepository<ISignalRepository>(); } }
        
        public SignalService(IStationUnitOfWork uow)
            : base(uow)
        { }

        public Signal Get(long Id)
        {
            return this.SignalRepository.Get(Id);
        }
    }
}
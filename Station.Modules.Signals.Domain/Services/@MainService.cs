using Station.Kernel.Infrastructure.Contracts;
using Station.Kernel.Infrastructure.Services;
using Station.Modules.Signals.Domain.Contracts;

namespace Station.Modules.Signals.Domain.Services
{
    public class MainService : KernelService<IStationUnitOfWork>
    {
        public MainService(IStationUnitOfWork uow) //IMainUnitOfWork
            : base(uow)
        {

        }
    }
}

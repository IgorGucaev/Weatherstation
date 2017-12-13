using Station.Common.Classes;
using Station.Common.Enums;
using Station.Modules.Signals.Infrastructure;
using Station.Modules.Signals.Domain.Contracts;
using Station.Modules.Signals.Domain.Contracts.Services;
using Station.Modules.Signals.Domain.Services;
using Station.Common.Contracts;

namespace Station.Modules.Signals.Api
{
    public class SignalsApiConfiguration : ApiConfiguration
    {
        public override void Config(AppMode mode, IDependencyRegistrator container, DependencyScope scope = DependencyScope.Scope)
        {
            // Unit of Work
            container.RegisterType<SignalUnitOfWork, IStationUnitOfWork>(DependencyScope.None);

            // Services
            container.RegisterType<SignalService, ISignalService>(scope);
        }
    }
}

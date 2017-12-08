using Station.Common.Classes;
using Station.Common.Enums;
using Station.Modules.Signals.Domain.Contracts;

namespace Station.Modules.Signals.Infrastructure
{
    public class StationApiConfiguration : AppConfiguration
    {
        public override void Config(AppMode mode, IDependencyRegistrator container, DependencyScope scope = DependencyScope.Scope)
        {
            container.RegisterType<StationUnitOfWork, IStationUnitOfWork>(DependencyScope.None);
        }
    }
}

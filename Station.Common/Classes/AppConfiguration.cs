using Station.Common.Contracts;
using Station.Common.Enums;

namespace Station.Common.Classes
{
    public abstract class AppConfiguration
    {
        public const DependencyScope DefaultDependencyScope = DependencyScope.Scope;

        public abstract void Config(AppMode mode, IDependencyRegistrator container, DependencyScope scope = DefaultDependencyScope);
    }
}

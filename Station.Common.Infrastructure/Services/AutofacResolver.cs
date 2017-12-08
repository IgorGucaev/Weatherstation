using Autofac;
using Station.Common.Contracts;
using Station.Common.Enums;

namespace Station.Common.Infrastructure.Services
{
    public class AutofacRegistrator : IDependencyRegistrator
    {
        private ContainerBuilder _builder;

        public AutofacRegistrator()
        {
            _builder = new ContainerBuilder();
        }

        public AutofacRegistrator(ContainerBuilder builder)
        {
            _builder = builder;
        }

        public void RegisterType<TService, TInterface>(DependencyScope scope = DependencyScope.Scope) where TService : TInterface
        {
            var reg = _builder.RegisterType<TService>().As<TInterface>();
            switch (scope)
            {
                case DependencyScope.None:
                    reg.InstancePerDependency();
                    return;
                case DependencyScope.Singletone:
                    reg.SingleInstance();
                    return;
                case DependencyScope.Scope:
                    reg.InstancePerLifetimeScope();
                    return;
                case DependencyScope.Request:
                    reg.InstancePerRequest();
                    return;
            }
        }

        public void RegisterInstance<TInterface>(TInterface service) where TInterface : class
        {
            _builder.RegisterInstance<TInterface>(service);
        }

        public IContainer Build()
        {
            return _builder.Build();
        }
    }
}
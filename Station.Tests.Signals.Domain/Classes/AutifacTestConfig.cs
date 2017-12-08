using Autofac;
using Station.Modules.Signals.Domain.Contracts;
using Station.Modules.Signals.Domain.Contracts.Repositories;
using Station.Modules.Signals.Domain.Contracts.Services;
using Station.Modules.Signals.Domain.Services;
using Station.Modules.Signals.Infrastructure;
using Station.Modules.Signals.Infrastructure.Repositories;

namespace Station.Tests.Signals.Domain.Classes
{
    public class AutofacTestConfig
    {
        static public IContainer ConfigureContainer()
        {
            // получаем экземпляр контейнера
            var builder = new ContainerBuilder();

            // регистрируем споставление типов
            builder.RegisterType<StationUnitOfWork>().As<IStationUnitOfWork>();
           // builder.RegisterType<SignalMongoBasedRepository>().As<ISignalRepository>();
            builder.RegisterType<SignalService>().As<ISignalService>();

            // создаем новый контейнер с теми зависимостями, которые определены выше
            var container = builder.Build();

            return container;
        }
    }
}

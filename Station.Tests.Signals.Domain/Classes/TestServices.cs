using Autofac;
using Station.Modules.Signals.Domain.Contracts;
using Station.Modules.Signals.Domain.Contracts.Services;
using Station.Modules.Signals.Infrastructure;
using System;

namespace Station.Tests.Signals.Domain.Classes
{
    public class TestServices : IDisposable
    {
        ISignalService _SignalService;
        IStationUnitOfWork _stationUOW = new SignalUnitOfWork();

        public ISignalService SignalService { get { return _SignalService; } }

        public IStationUnitOfWork StationUnitOfWork
        { get { return _stationUOW; } }

        public TestServices()
        {
            var container = AutofacTestConfig.ConfigureContainer();
            _SignalService = container.Resolve<ISignalService>();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

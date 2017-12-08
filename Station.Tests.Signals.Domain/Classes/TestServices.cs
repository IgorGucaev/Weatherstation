using Station.Modules.Signals.Domain.Contracts;
using Station.Modules.Signals.Domain.Contracts.Services;
using Station.Modules.Signals.Domain.Services;
using Station.Modules.Signals.Infrastructure;
using System;

namespace Station.Tests.Signals.Domain.Classes
{
    public class TestServices : IDisposable
    {
        ISignalService _SignalService;
        IStationUnitOfWork _lvUOW = new StationUnitOfWork();

        public ISignalService SignalService { get { return _SignalService; } }

        public IStationUnitOfWork LightVersionUnitOfWork
        { get { return _lvUOW; } }

        public TestServices()
        {
            _SignalService = new SignalService(_lvUOW);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

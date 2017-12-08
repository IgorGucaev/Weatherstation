using Autofac;
using Station.Common.Infrastructure.Services;

namespace Station.Tests.Signals.Domain.Classes
{
    public class DomainTest
    {
        protected TestServices _services;

        public DomainTest()
        {
            _services = new TestServices();
        }
    }
}

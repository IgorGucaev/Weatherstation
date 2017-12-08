using Autofac;

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
